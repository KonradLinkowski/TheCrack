using UnityEngine;
using System.Collections;
using Konrad.Utility;

public class RotacjaKamery : MonoBehaviour {

	public Transform leftTarget;
	public Transform rightTarget;
	public Transform startTarget;
	public Transform player;
	
	RaycastHit hitPoint;

	public float speed;

	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			player.position = Vector3.Lerp (player.position, leftTarget.position, speed * Time.deltaTime);
			player.rotation = Quaternion.Lerp (player.rotation, leftTarget.rotation, speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.D)) {
			player.position = Vector3.Lerp (player.position, rightTarget.position, speed * Time.deltaTime);
			player.rotation = Quaternion.Lerp (player.rotation, rightTarget.rotation, speed * Time.deltaTime);
		} else {
			player.position = Vector3.Lerp (player.position, startTarget.position, speed * Time.deltaTime);
			player.rotation = Quaternion.Lerp (player.rotation, startTarget.rotation, speed * Time.deltaTime);
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hitPoint, 10)) {
				Debug.Log ("ray casted");
				if (hitPoint.transform.gameObject.name == "ArenaPortal") {
					Application.LoadLevel (1);
					Debug.Log ("Load level");
				}
			}
		}
	}
}
