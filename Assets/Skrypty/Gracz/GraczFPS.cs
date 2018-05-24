using UnityEngine;
using System.Collections;
using Konrad.Utility;
using Konrad.Weapons;
using UnityEngine.UI;

public class GraczFPS : MonoBehaviour {

	// Zdrowie:
	public int startingHealth = 100;        // Startowa ilość punktów zdrowia.
	public float currentHealth;               // Aktualne ilość punktów zdrowia.

	// Szybkość poruszania się:
	public float speed = 6f;            	// Szybkość z jaką porusza się gracz.
	public Transform t_eyes;					// Oczy gracza.

	public Camera kamera;

	public Image[] celownictwo = new Image[4];

	//Broń:
	public GameObject gun;

	public MouseLook mouseLook;

	[HideInInspector] public int equipedWeaponNumber = 0;

	public GameObject bronie;

	public Canvas celowniki;

	public int healPerSecond;

	public Animator death;

	float resetTimer;

	Vector3 movement;                   // Wektor kierunku poruszania się gracza.
	//public static Animator anim;                      // Referencja do animatora.
	Rigidbody playerRigidbody;          // Referencja do rigidbody.
	bool isDead;                        // Wartość prawdziwa, gdy gracz jest martwy.
	//bool damaged;                       // Wartość prawdziwa, jeśli gracz został zraniony.

	[HideInInspector] public bool playing = true;

	float equipTime;
	bool equiping;

	bool menu;

	void Start () {
#if !UNITY_EDITOR
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
#endif
		ListaBroni.Setup ();
		gun = ListaBroni.listOfWeapons [1].gameObject;
		gun.SetActive (true);
		gun.GetComponent <BronS> ().obrazek.color = Color.yellow;
		mouseLook.Init (transform, t_eyes);
		// Ustawianie referencji.
		//anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();

		currentHealth = startingHealth; //Ustawianie zdrowia.
	}

	void Update () {
		if (Input.GetKey (KeyCode.P)) {
			resetTimer += Time.deltaTime;
			if (resetTimer > 5)
				FinalS.ResetScores ();
		}
		if (currentHealth < startingHealth && currentHealth > 0)
			currentHealth += healPerSecond * Time.deltaTime;
		if (currentHealth > startingHealth)
				currentHealth = startingHealth;
		if (Input.GetKeyDown (KeyCode.O)) {
			menu = !menu;
			playing = !playing;
		}

		// Obracania gracza oraz broni.
		if (playing) {
			Turning ();
			float wheelInput = Input.GetAxisRaw ("Mouse ScrollWheel");
			
			if (wheelInput > 0) {
				EquipWeapon (1);
			} else if (wheelInput < 0) {
				EquipWeapon (-1);
			}

			celowniki.enabled = true;
		} else {
			celowniki.enabled = false;
		}
		if (equiping) {
			equipTime += Time.deltaTime;
			bronie.SetActive (true);
			if (equipTime >= 1) {
				equiping = false;
				bronie.SetActive (false);
				equipTime = 0;
			}
		}

		if (FinalS.theEnd)
			FinalS.Update ();
		if (isDead) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void EquipWeapon (int value) {
		equipedWeaponNumber += value;
		if (equipedWeaponNumber == -1)
			equipedWeaponNumber = ListaBroni.boughtItems.Count - 1;
		else if (equipedWeaponNumber == ListaBroni.boughtItems.Count)
			equipedWeaponNumber = 0;
		foreach (Image imd in celownictwo)
			imd.enabled = false;
		celownictwo [0].enabled = true;
		gun.GetComponent <BronS> ().obrazek.color = Color.white;
		gun.SetActive (false);
		gun = ListaBroni.listOfWeapons [ListaBroni.boughtItems [equipedWeaponNumber]].gameObject;
		gun.SetActive (true);
		gun.GetComponent <BronS> ().obrazek.color = Color.yellow;
		equiping = true;
		kamera.fieldOfView = 45;
	}

	void Turning () {
		mouseLook.LookRotation (transform, t_eyes);
	}


	void FixedUpdate ()
	{
		// Zbieranie osi z klawiatury.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (currentHealth <= 0) {
			playing = false;

		} else {
			if (VendorS.open)
				playing = false;
			else
				playing = true;
		}

		// Animowanie gracza.
		//Animating (h, v);
		if (playing) {
			// Poruszanie gracza po scenie.
			Move (h, v);
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
		death.SetTrigger ("Smierc");
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		// Przekazanie animatorowi, że gracz jest martwy.
		//anim.SetTrigger ("Die");
	}
	
	void Move (float h, float v)
	{
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 12f;
		} else
			speed = 6f;

		// Ustawianie wektora na podstawie osi.
		movement.Set (h, 0f, v);
		// Zmiana zmiennych wektora z lokalnych na globalne.
		movement = transform.TransformDirection (movement);
		// Normalizacja.
		movement = movement.normalized * speed * Time.deltaTime;
		// Poruszanie gracza z aktualnej pozycji do zawartej w wektorze.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Animating (float h, float v)
	{
		// Tworzenie zmiennej logiczej przyjmującej wartość prawdziwą, gdy gracz jest w ruchu.
		//bool walking = h != 0f || v != 0f;
		
		// Przekazuje animatorowi czy gracz się porusza czy nie.
		//anim.SetBool ("IsWalking", walking);
	}
}
