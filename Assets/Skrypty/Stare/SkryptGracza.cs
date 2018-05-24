using UnityEngine;
using System.Collections;

public class SkryptGracza : MonoBehaviour {

	public int startingHealth = 100;        // Startowa ilość punktów zdrowia.
	public int currentHealth;               // Aktualne ilość punktów zdrowia.

	public float speed = 6f;            	// Szybkość z jaką porusza się gracz.
	public float yRotationSpeed = 800f;		// Rotacja pionowa gracza.
	public float xRotationSpeed = 800f;		// Rotacja pozioma gracza.
	public Transform gun;					// Broń, którą trzyma gracz.

	public int raysNumber = 10;			// Liczba promieni wychodzących z broni.
	public float timeBetweenShots = 3; 	// Czas na strzał.
	public Transform barrel;			// Pozycja lufy.
	public int damage = 5;				// Obrażenie zadawane przez pojedyńczy promień.

	public Camera cameraBehind;
	public Camera cameraBarrel;

	RaycastHit[] cele;
	float timer = 0;
	
	Vector3 movement;                   // Wektor kierunku poruszania się gracza.
	Animator anim;                      // Referencja do animatora.
	Rigidbody playerRigidbody;          // Referencja do rigidbody.
	bool isDead;                        // Wartość prawdziwa, gdy gracz jest martwy.
	//bool damaged;                       // Wartość prawdziwa, jeśli gracz został zraniony.


	void Start () {
		// Ustawianie referencji.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();

		currentHealth = startingHealth; //Ustawianie zdrowia.

		cele = new RaycastHit [raysNumber];
	}

	void Update () {
		timer += Time.deltaTime;
		
		if (Input.GetButtonDown ("Fire1") && timer >= timeBetweenShots) {
			Shoot ();
		}


	}

	void Shoot () {
		timer = 0;
		
		for (int i = 0; i < raysNumber; i++) {
			Vector3 kierunek = new Vector3 (Random.Range (0, 5), Random.Range (0, 5), 10);
			kierunek = barrel.TransformDirection (kierunek);
			if (Physics.Raycast (barrel.position, kierunek, out cele[i], 10)) {
				// Try and find an EnemyHealth script on the gameobject hit.
				SkryptPrzeciwnika enemyHealth = cele[i].collider.GetComponent <SkryptPrzeciwnika> ();
				
				// If the EnemyHealth component exist...
				if(enemyHealth != null)
				{
					// ... the enemy should take damage.
					enemyHealth.TakeDamage (damage, cele[i].point, kierunek);
				}
				
			}
			Debug.DrawRay (barrel.position, kierunek, Color.red);
		}
	}

	void FixedUpdate ()
	{
		// Zbieranie osi z klawiatury.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		CameraChange ();

		// Poruszanie gracza po scenie.
		Move (h, v);
		
		// Obracania gracza oraz broni.
		Turning ();
		
		// Animowanie gracza.
		Animating (h, v);
	}

	void CameraChange () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			cameraBarrel.enabled = true;
			cameraBehind.enabled = false;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			cameraBarrel.enabled = false;
			cameraBehind.enabled = true;
		}
	}

	public void TakeDamage (int amount) {
		// Zmienna przyjmuje wartość prawdziwą, aby pokazać zranienie.
		//damaged = true;
		
		// Redukcja aktualnego zdrowia o otrzymaną wartość obrażeń.
		currentHealth -= amount;

		// Jeśli gracz stracił całe swoje zdrowie, lecz jeszcze nie jest martwy.
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death () {
		// Ustawienie wartości prawdziwej, aby funkcja nie została wykonana ponownie.
		isDead = true;

		// Przekazanie animatorowi, że gracz jest martwy.
		anim.SetTrigger ("Die");
		
		// Wyłączenie poruszania się i strzelania.
			//skrypty...
	}

	void Move (float h, float v)
	{
		// Ustawianie wektora na podstawie osi.
		movement.Set (h, 0f, v);
		// Zmiana zmiennych wektora z lokalnych na globalne.
		movement = transform.TransformDirection (movement);
		// Normalizacja.
		movement = movement.normalized * speed * Time.deltaTime;
		// Poruszanie gracza z aktualnej pozycji do zawartej w wektorze.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		// Zapis przesunięcia myszy.
		float horizontalMouseInput = Input.GetAxisRaw ("Mouse X");
		float verticalMouseInput = Input.GetAxisRaw ("Mouse Y");
		// Obrót wzdłuż osi Y.
		transform.RotateAround (transform.position, Vector3.up, horizontalMouseInput * yRotationSpeed * Time.deltaTime);
		// Obrót wzdłuż osi X.

		if (gun.localRotation.eulerAngles.x < 335 && gun.localRotation.eulerAngles.x > 155)
			gun.localRotation.Set (-0.2f, 0.0f, 0.0f, 1.0f);
		else if (gun.localRotation.eulerAngles.x > 25 && gun.localRotation.eulerAngles.x <= 155)
			gun.localRotation.Set (0.2f, 0.0f, 0.0f, 1.0f);

		gun.RotateAround (gun.position, -gun.right, verticalMouseInput * xRotationSpeed * Time.deltaTime);
	}

	void Animating (float h, float v)
	{
		// Tworzenie zmiennej logiczej przyjmującej wartość prawdziwą, gdy gracz jest w ruchu.
		bool walking = h != 0f || v != 0f;
		
		// Przekazuje animatorowi czy gracz się porusza czy nie.
		anim.SetBool ("IsWalking", walking);
	}
}
