using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LizakS : MonoBehaviour {

	public int damage;

	UnityEngine.AI.NavMeshAgent navMesh;
	GraczFPS gracz;
	ParticleSystem particle;

	float timer;
	public float minTime;

	RaycastHit hit;

	void Start () {
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		gracz = GameObject.FindGameObjectWithTag ("Player").GetComponent <GraczFPS> ();
		particle = transform.FindChild ("Particle").GetComponent <ParticleSystem> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (gracz.currentHealth > 0) {
			Physics.Raycast (transform.position, gracz.transform.position - transform.position, out hit, 1000);
			if (hit.transform.gameObject.CompareTag ("Player")) {
				navMesh.SetDestination (gracz.transform.position);
				//SerializedObject so = new SerializedObject (particle);
				//so.ApplyModifiedProperties ();
				particle.enableEmission = true;
				if (Vector3.Distance (transform.position, gracz.transform.position) <= 10f) {
					if (timer > minTime) {
						gracz.TakeDamage (damage);
						timer = 0;
					}
					navMesh.speed = 0.5f;
					//so.FindProperty ("ShapeModule.radius").floatValue = 10f;
					particle.emissionRate = 500f;
					//so.ApplyModifiedProperties ();
					if (Vector3.Distance (transform.position, gracz.transform.position) <= 2f)
						navMesh.speed = 0f;
				} else {
					navMesh.speed = 2f;
					//so.FindProperty ("ShapeModule.radius").floatValue = 1f;
					particle.emissionRate = 50f;
					//so.ApplyModifiedProperties ();
				}
			} else {
				particle.enableEmission = false;
				navMesh.speed = 2f;
				navMesh.SetDestination (gracz.transform.position);
			}
		}
	}
}
