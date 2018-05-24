using UnityEngine;
using System.Collections;

public class CukierS : MonoBehaviour {

	GraczFPS gracz;
	UnityEngine.AI.NavMeshAgent navMesh;
	Transform model;
	public float rotationSpeed;
	public GameObject kubik;
	public float timeBetweenShots;
	float timer;

	float soundTimer;
	public float soundTime;

	public AudioClip[] sounds = new AudioClip[0];
	AudioSource audio;


	void Start () {
		audio = GetComponent <AudioSource> ();
		audio.clip = sounds [Random.Range (0, sounds.Length)];
		gracz = GameObject.Find ("Gracz").GetComponent <GraczFPS> ();
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		model = transform.FindChild ("Cukierek-Model");
	}


	void Update () {
		soundTimer += Time.deltaTime;
		if (soundTimer >= soundTime) {
			audio.clip = sounds [Random.Range (0, sounds.Length)];
			audio.Play ();
			soundTimer = 0;
		}
		if (gracz.currentHealth > 0) {
			if (Vector3.Distance (gracz.gameObject.transform.position, transform.position) < 10
			    && Physics.Raycast (transform.position, gracz.gameObject.transform.position - transform.position, 1000)) {
				if (!Physics.Raycast (model.position, -model.up, 1f)) {
					navMesh.enabled = true;
					model.RotateAround (model.position, model.right, 360 * Time.deltaTime);
					navMesh.SetDestination (gracz.transform.position);
				} else {
					timer += Time.deltaTime;
					navMesh.enabled = false;
					Vector3 direction = (gracz.transform.position - transform.position).normalized;
					Quaternion lookRotation = Quaternion.LookRotation (direction);
					transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
					if (timer >= timeBetweenShots) {
						Instantiate (kubik, transform.position, transform.rotation);
						timer = 0;
					}
				}
			} else {
				navMesh.enabled = true;
				model.RotateAround (model.position, model.right, 360 * Time.deltaTime);
				navMesh.SetDestination (gracz.transform.position);
			}
		}
	}
}
