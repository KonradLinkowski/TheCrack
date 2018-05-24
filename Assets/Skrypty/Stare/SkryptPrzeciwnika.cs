using UnityEngine;
using System.Collections;

public class SkryptPrzeciwnika : MonoBehaviour {

	public int startingHealth = 100;            // Startowa ilość zdrowia.
	public int currentHealth;                   // Aktualne zdrowie.
	public float sinkSpeed = 2.5f;              // Szybkość z jaką przeciwnik spływa w dół po śmierci.
	public int scoreValue = 10;                 // Ilość punktów otrzymywana za zabicie przeciwnika.

	public float timeBetweenAttacks = 0.5f;     // Czas pomiędzy atakami.
	public int attackDamage = 10;               // Wartość obrażeń.

	public GameObject player;			// Referencja do gracza.

	bool playerInRange;                         // Prawda, jeśli gracz jest w zasięgu ataku.
	float timer;                                // Timer do liczenia ile do następnego ataku.

	ParticleSystem hitParticles;

	CapsuleCollider capsuleCollider;    // Referencja do collidera.
	Animator anim;                      // Referencja do animatora.
	SkryptGracza playerS;				// Referencja do skryptu gracza.
	UnityEngine.AI.NavMeshAgent nav;               	// Referencja do nawigacji.
	bool isDead;                        // Prawda, jeśli przeciwnik jest martwy.
	bool isSinking;                     // Prawda, jeśli przeciwnik spływa.
	
	void Start () {
		// Ustawianie referencji.
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		playerS = player.GetComponent <SkryptGracza> ();
		anim = GetComponent <Animator> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		hitParticles = GetComponent <ParticleSystem> ();
		
		// Ustawianie zdrowia.
		currentHealth = startingHealth;
	}

	void Update () {
		// Dodanie czasu, który minął od ostatniej klatki do timera.
		timer += Time.deltaTime;
		
		// Jeśli minął odpowiedni czas, gracz jest w zagięgu i przeciwnik jest ciągle żywy.
		if(timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
		{
			Attack ();
		}
		
		//Jeśli gracz ma 0 lub mnie zdrowia.
		if (playerS.currentHealth <= 0)
		{
			// Przekazywanie animatorowi, że gracz jest martwy.
			anim.SetTrigger ("PlayerDead");
		}

		// Jeśli poziom zdrowia gracza jest większy niż 0.
		if (playerS.currentHealth > 0 && currentHealth > 0) {
			// Ustawienie celu nawigacji na gracza.
			nav.SetDestination (player.transform.position);
		} else {
			// Wyłączenie nawigacji.
			nav.enabled = false;
		}

		// Gdy gracz powinien spływać.
		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	void Attack ()
	{
		// Reset timera.
		timer = 0f;
		
		// Jeśli gracz jest wciąż żywy.
		if(playerS.currentHealth > 0)
		{
			anim.SetTrigger ("Attack");
			// Zadawanie obrażeb graczowi.
			playerS.TakeDamage (attackDamage);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		// Jeśli gracz przecina collider.
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		// Jeśli gracz przestaje przecinać collider.
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint, Vector3 direcion)
	{
		// Jeśli przeciwnik jest martwy.
		if(isDead)
			// Wyjście z funkcji.
			return;
		
		// Redukcja aktualnego zdrowia o otrzymaną wartość obrażeń.
		currentHealth -= amount;

		GetComponent <Rigidbody> ().AddForceAtPosition (direcion / 5, hitPoint, ForceMode.Impulse);

		// Pojawia odłamki w miejscu, w którym został trafiony.
		hitParticles.transform.position = hitPoint;
		
		// Pokazuje animacje.
		hitParticles.Play();
		
		// Jeśli aktualne zdrowie jest mniejsze lub równe 0.
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		// Przeciwnik jest martwy,
		isDead = true;
		
		// Zmiena collider na trigger więc strzały mogą przejść przez niego.
		capsuleCollider.isTrigger = true;
		
		// Przekazanie animatorowi, że przeciwnik jest martwy.
		anim.SetTrigger ("Dead");
		
		// Zmiana audio z dźwięku obrażeń na dźwięk śmierci.
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		StartSinking ();
	}

	void StartSinking ()
	{
		// Znajdowanie i wyłączenie nawigacji.
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		
		// Znajdowanie rigidbody i zmienienie go na kinematyczne.
		GetComponent <Rigidbody> ().isKinematic = true;
		
		// Przeciwnik powinien spływać.
		isSinking = true;
		
		// Zwiększenie punktów gracza o wartość.
		//ScoreManager.score += scoreValue;
		
		// Zniszcz przeciwnika po 2 sekundach.
		Destroy (gameObject, 2f);
	}
}
