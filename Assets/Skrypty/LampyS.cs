using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LampyS : MonoBehaviour {

	float timer;
	public float szybkosc;

	List <Light> lampy = new List <Light> ();

	void Start () {
		lampy.AddRange (GameObject.FindObjectsOfType <Light> ());
		for (int i = 0; i < lampy.Count; i++) {
			if (lampy [i].color == Color.white) {
				lampy.Remove (lampy [i]);
				i--;
			}
			lampy [i].color = new Color (Random.value, Random.value, Random.value);
		}
	}

	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= 0.1) {
			foreach (Light lampa in lampy) {
				float blue = lampa.color.b + (szybkosc * Time.deltaTime * 0.05f + Random.Range (0, 0.05f));
				if (blue > 1)
					blue = 0;
				lampa.color = new Color (1, 0, blue);
			}
			timer = 0;
		}
	}
}
