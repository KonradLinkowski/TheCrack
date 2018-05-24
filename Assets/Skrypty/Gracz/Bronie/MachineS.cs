using UnityEngine;
using System.Collections;
using Konrad.Weapons;

public class MachineS : BronS {

	public float lightTime;
	public float maxLightTime = 0.2f;

	Vector3 kierunek;

	public float spreadTime;

	void Start () {
		heldInMagizine = magazineCapacity;
		t_barrel = transform.FindChild ("Lufa");
		s_Shot = t_barrel.GetComponent <AudioSource> ();
		s_Reload = GetComponent <AudioSource> ();
		l_shot = t_barrel.GetComponent <Light> ();
		gracz = transform.parent.parent.GetComponent <GraczFPS> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (Input.GetButton ("Fire1") && timer >= timeBetweenShots && gracz.playing && heldInMagizine > 0) {
			//ScreenCast ();
			Shot ();
		}
		if (Input.GetButtonUp ("Fire1")) {
			spreadTime = 0;
		}
		
		if (l_shot.enabled) {
			lightTime += Time.deltaTime;
			if (lightTime >= maxLightTime) {
				l_shot.enabled = false;
				lightTime = 0;
			}
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

	public void Shot () {
		timer = 0;
		s_Shot.Play ();
		l_shot.enabled = true;

		spreadTime += 0.3f;
		if (spreadTime > 5)
			spreadTime = 5;
		kierunek = t_spread.position + UnityEngine.Random.insideUnitSphere * (splash / 20 + spreadTime);
		t_barrel.LookAt (kierunek);
		Instantiate (g_kubik, t_barrel.position, t_barrel.rotation);
		heldInMagizine--;
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
