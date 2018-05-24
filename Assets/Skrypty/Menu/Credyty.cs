using UnityEngine;
using System.Collections;

public class Credyty : MonoBehaviour {

	public Vector3[] miejsca = new Vector3[0];
	int miejsce;
	float timer;
	public float minimumTime;

	void Start () {
		timer = 0;
		miejsce = 0;
		Time.timeScale = 1;
	}

	void Update () {
		transform.position = Vector3.Lerp (transform.position, miejsca [miejsce], Time.deltaTime);
		timer += Time.deltaTime;
		if (Input.GetButtonDown ("Fire1") || timer >= minimumTime) {
			miejsce++;
			timer = 0;
		}
		if (miejsce >= miejsca.Length) {
			Application.Quit ();
		}
	}
}
