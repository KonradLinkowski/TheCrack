using UnityEngine;
using System.Collections;
using Konrad.Utility;

public class Gracz : MonoBehaviour {

	public MouseLook mouseLook;
	public Camera kamera;
	Rigidbody playerRigidbody;
	public float speed;
	Vector3 movement;
	public Canvas quitGame;

	void Start () {
		mouseLook.Init (transform, kamera.transform);
		playerRigidbody = GetComponent <Rigidbody> ();
		movement = new Vector3 ();
#if !UNITY_EDITOR
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
#endif
	}

	void FixedUpdate () {
		mouseLook.LookRotation (transform, kamera.transform);

		float h, v;
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		if (h != 0 || v != 0) {
			quitGame.enabled = false;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
		}

		// Ustawianie wektora na podstawie osi.
		movement.Set (h, 0f, v);
		// Zmiana zmiennych wektora z lokalnych na globalne.
		movement = transform.TransformDirection (movement);
		// Normalizacja.
		movement = movement.normalized * speed * Time.deltaTime;
		// Poruszanie gracza z aktualnej pozycji do zawartej w wektorze.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Update () {
		if (Input.GetButton ("Fire2")) {
			kamera.fieldOfView = 30;
		}
		if (Input.GetButtonUp ("Fire2")) {
			kamera.fieldOfView = 60;
		}
		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.E)) {
			Ray ray = kamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.transform.gameObject.CompareTag ("Teleport")) {
					Application.LoadLevel ("Level1");
				}
				if (hit.transform.gameObject.name == "drzwi") {
					quitGame.enabled = true;
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}
	}

	public void Quit () {
		Application.LoadLevel ("Credits");
	}

	public void Resume () {
		quitGame.enabled = false;
	}
}
