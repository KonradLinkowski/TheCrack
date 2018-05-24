using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Konrad.Utility {

	public class FinalS {

		static float timer;
		static Transform bohaterka;
		static Transform gracz;
		public static bool theEnd;


		public static void TheEnd () {
			bohaterka = GameObject.FindGameObjectWithTag ("Finish").transform;
			gracz = GameObject.FindGameObjectWithTag ("Player").transform;
			PlayerPrefs.Save ();
			theEnd = true;
		}
		public static void SaveScores (string key, float value) {
			PlayerPrefs.SetFloat (key, value);
			SaveScores ();
		}

		public static void SaveScores () {
			PlayerPrefs.Save ();
		}


		public static void Update () {
			timer += Time.deltaTime;

			if (Vector3.Distance (gracz.position, bohaterka.position) < 3)
				Application.LoadLevel ("Credits");
		}

		public static void ResetScores () {
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.Save ();
		}
	}

}