using UnityEngine;
using System.Collections;
using Konrad.Weapons;
using UnityEngine.UI;

public class SnajperkaS : BronS {

	public Camera kamera;
	public Image[] zoom = new Image[4];
	Vector3 pozycja;

	void Start () {
		pozycja = transform.localPosition;
		heldInMagizine = magazineCapacity;
		t_barrel = transform.FindChild ("Lufa");
		s_Shot = t_barrel.GetComponent <AudioSource> ();
		s_Reload = GetComponent <AudioSource> ();
		l_shot = t_barrel.GetComponent <Light> ();
		gracz = transform.parent.parent.GetComponent <GraczFPS> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (Input.GetButtonDown ("Fire1") && timer >= timeBetweenShots && heldInMagizine > 0 && gracz.playing) {
			Aim ();
			timer = 0;
		}
		if (Input.GetButtonDown ("Fire2")) {
			for (int i = 0; i < zoom.Length; i++)
				zoom [i].enabled = !zoom [i].enabled;
			zoom [0].enabled = !zoom [0].enabled;
			kamera.fieldOfView = kamera.fieldOfView == 45 ? 25 : 45;
			transform.localPosition = transform.localPosition == pozycja ? new Vector3 (0, 0.6f, 0) : pozycja;
		}
		if (heldInMagizine <= 0)
			reloading = true;
		if (Input.GetKeyDown (KeyCode.R)) {
			reloading = true;
		}
		
		if (reloading) {
			Reload ();
		}
	}

	void Aim () {
		Ray ray = kamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			Shot (hit.point);
		}
	}

	void Shot (Vector3 hitPoint) {
		s_Shot.Play ();
		RaycastHit info;
		t_barrel.LookAt (hitPoint);
		if (Physics.Raycast (t_barrel.position, t_barrel.forward, out info, 100)) {
			Instantiate (g_kubik, info.point, t_barrel.rotation);
			heldInMagizine--;
		}
	}

	void Reload () {
		reloadTimer += Time.deltaTime;
		if (reloadTimer >= reloadTime) {
			heldInMagizine = magazineCapacity;
			reloadTimer = 0;
			reloading = false;
			s_Reload.Play ();
		}
	}
}
