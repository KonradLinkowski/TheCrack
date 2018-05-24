using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Konrad.Weapons {
	[Serializable]
	public class ShotgunS : BronS {

		public int raysNumber = 20;
		public Vector3 kierunek;

		public Camera kamera;

		public RaycastHit[] cele;

		public float lightTime;
		public float maxLightTime;

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
			if (Input.GetButtonDown ("Fire1") && timer >= timeBetweenShots && heldInMagizine > 0 && gracz.playing) {
				//ScreenCast ();
				Shot ();
			}

			if (l_shot.enabled) {
				lightTime += Time.deltaTime;
				if (lightTime >= maxLightTime)
					l_shot.enabled = false;
				lightTime = 0;
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

			for (int i = 0; i < raysNumber; i++) {
				kierunek = t_spread.position + UnityEngine.Random.insideUnitSphere * splash;
				t_barrel.LookAt (kierunek);
				Instantiate (g_kubik, t_barrel.position, t_barrel.rotation);
			}
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
}
