using UnityEngine;
using System.Collections;

public class RabbitS : MonoBehaviour {

	public int damage = 5;

	GraczFPS gracz;
	UnityEngine.AI.NavMeshAgent navMesh;
	Animator anim;
	
	float timer = 0;
	float timeBetweenJumps = 0.5f;

	bool atak;

	void Start () {
		gracz = GameObject.Find ("Gracz").GetComponent <GraczFPS> ();
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}

	void Update () {
		if (gracz.currentHealth > 0) {
			if (timer >= timeBetweenJumps) {
				anim.SetTrigger ("Jump");
				timer = 0;
			} else {
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Skok")) {
					if (!atak && Vector3.Distance (transform.position, gracz.transform.position) <= 3) {
						atak = true;
						gracz.TakeDamage (damage);
					}
					navMesh.enabled = true;
					navMesh.SetDestination (gracz.gameObject.transform.position);
					timer = 0;
				} else {
					atak = false;
					navMesh.enabled = false;
					timer += Time.deltaTime;
				}
			}
		}
	}
	void OnTriggerEnter (Collider player) {
		if (player.CompareTag ("Player") && anim.GetCurrentAnimatorStateInfo (0).IsName ("Skok")) {
			player.GetComponent <GraczFPS> ().TakeDamage (damage);
		}
	}
}
