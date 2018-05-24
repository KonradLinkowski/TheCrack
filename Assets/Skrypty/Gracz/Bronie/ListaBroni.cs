using UnityEngine;
using System.Collections;
using Konrad.Weapons;
using System.Collections.Generic;

namespace Konrad.Weapons {
	public class ListaBroni {
		public static GameObject t_eyes;
		public static ShotgunS shotgun;
		public static PistolS pistol;
		public static ShotgunS shogun;
		public static MachineS machine;
		public static SMGS smg;
		public static GranatnikS granatnik;
		public static SnajperkaS snajperka;
		public static BronS [] listOfWeapons = new BronS [6];

		public static List <int> boughtItems = new List <int> ();

		public static void Setup () {
			t_eyes = GameObject.Find ("Gracz/Oczy");
			shotgun = t_eyes.transform.FindChild ("Shotgun").GetComponent <ShotgunS> ();
			pistol = t_eyes.transform.FindChild ("Pistolet").GetComponent <PistolS> ();
			machine = t_eyes.transform.FindChild ("Karabin").GetComponent <MachineS> ();
			smg = t_eyes.transform.FindChild ("SMG").GetComponent <SMGS> ();
			granatnik = t_eyes.transform.FindChild ("Granatnik").GetComponent <GranatnikS> ();
			snajperka = t_eyes.transform.FindChild ("Snajperka").GetComponent <SnajperkaS> ();

			boughtItems.Add (1);

			listOfWeapons [0] = pistol;
			listOfWeapons [1] = shotgun;
			listOfWeapons [2] = machine;
			listOfWeapons [3] = smg;
			listOfWeapons [4] = granatnik;
			listOfWeapons [5] = snajperka;
		}
	}
}