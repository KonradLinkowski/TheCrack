using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Konrad.Weapons {

	public class BronS : MonoBehaviour {
	
		public int layer = 256;
		public float splash;
		public float timeBetweenShots; 	// Czas na strzał.
		[HideInInspector] public float timer;

		public GameObject g_kubik;			// Testowy śrut.
		[HideInInspector] public Transform t_barrel;			// Pozycja lufy.
		[HideInInspector] public AudioSource s_Shot;
		[HideInInspector] public AudioSource s_Reload;
		[HideInInspector] public Light l_shot;

		public int magazineCapacity;
		[HideInInspector] public int heldInMagizine;
		public float reloadTime;
		[HideInInspector] public float reloadTimer;

		public bool kupiona;
		public int cena;
		public string nazwa;
		public string opis;
		public float celnosc;
		public float szybkoscPrzeladowania;
		public float szybkoscAtaku;
		public Image obrazek;

		[HideInInspector] public bool reloading;

		public Transform t_spread;

		[HideInInspector] public GraczFPS gracz;

		RaycastHit cel;

	}
}