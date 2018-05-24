using UnityEngine;
using System.Collections;

public class KotS : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent navMesh;
	GraczFPS gracz;
	float timer;
	float timerBetweenRushes;
	public float timeBetweenRushes;
	public float timeBetweenAttacks;
	float rushTimer;
	public float rushTime;
	bool rush;
	public float rushSpeed;
	public float speed;

	public int damage;

	Vector3 pozycja;

	void Start () {
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		gracz = GameObject.FindGameObjectWithTag ("Player").GetComponent <GraczFPS> ();
	}

	void Update () {
		if (gracz.currentHealth > 0) {
			timer += Time.deltaTime;
			if (!rush) {
				navMesh.SetDestination (gracz.transform.position);
				timerBetweenRushes += Time.deltaTime;
				if (Vector3.Distance (transform.position, gracz.transform.position) <= 10 && timerBetweenRushes >= timeBetweenRushes) {
					rush = true;
					navMesh.speed = rushSpeed;
					timerBetweenRushes = 0;
				}
			} else {
				rushTimer += Time.deltaTime;
				pozycja = transform.position + transform.forward;
				navMesh.SetDestination (pozycja);
				if (rushTimer >= rushTime) {
					rush = false;
					navMesh.speed = speed;
					rushTimer = 0;
				}
			}
			if (timer >= timeBetweenAttacks && Vector3.Distance (transform.position, gracz.transform.position) <= 2) {
				gracz.TakeDamage (damage);
				timer = 0;
			}
		}
	}
}
