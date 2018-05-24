using UnityEngine;
using System.Collections;

public class MotylS : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent navMesh;
	float timer;
	GraczFPS gracz;
	public float timeBetweenShots;

	float burstTimer;
	public float burstTime;

	Vector3 kierunek;

	public GameObject kubik;

	Transform t_barrel;

	public int liczbaPociskow;

	float stopTimer;
	public float stopTime;

	float soundTimer;
	public float soundTime;
	
	public AudioClip[] sounds = new AudioClip[0];
	AudioSource audio;

	void Start () {
		audio = GetComponent <AudioSource> ();
		audio.clip = sounds [Random.Range (0, sounds.Length)];
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		gracz = GameObject.FindGameObjectWithTag ("Player").GetComponent <GraczFPS> ();
		t_barrel = transform.FindChild ("Lufa");
	}

	void Update () {
		soundTimer += Time.deltaTime;
		if (soundTimer >= soundTime) {
			audio.clip = sounds [Random.Range (0, sounds.Length)];
			audio.Play ();
			soundTimer = 0;
		}
		if (gracz.currentHealth > 0) {
			timer += Time.deltaTime;
			if (Vector3.Distance (transform.position, gracz.transform.position) <= 3 && timer > timeBetweenShots) {
				gracz.TakeDamage (15);
			}

			navMesh.SetDestination (gracz.transform.position);

			burstTimer += Time.deltaTime;
			if (burstTimer >= burstTime) {
				burstTimer = 0;
				for (int i = 0; i < liczbaPociskow; i++) {
					kierunek = transform.position + UnityEngine.Random.insideUnitSphere;
					t_barrel.LookAt (kierunek);
					Instantiate (kubik, t_barrel.position, t_barrel.rotation);
				}
			}
		}
	}
}
