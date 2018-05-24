using UnityEngine;
using System.Collections;

public class GranatS : MonoBehaviour {

	Rigidbody rb;
	public float speed = 5;
	Vector3 prosto;
	ParticleSystem particles;
	bool isParticles;
	float timer;
	public float deathTime = 5;
	public int damage;
	public AudioSource boom;


	void Start () {
		prosto = transform.InverseTransformDirection (transform.forward);
		rb = GetComponent <Rigidbody> ();
		rb.AddRelativeForce (prosto * speed, ForceMode.VelocityChange);
		particles = GetComponent <ParticleSystem> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (isParticles && !particles.isPlaying) {
			Destroy (gameObject);
		}

		if (timer >= deathTime)
			Destroy (gameObject);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer != 8 || other.gameObject.CompareTag ("Player"))
			return;
		GetComponent<Collider>().gameObject.GetComponent <Collider> ().enabled = false;
		foreach (Collider colider in Physics.OverlapSphere (transform.position, 10)) {
			if (!colider.gameObject.CompareTag ("Player")) {
				ZdrowiePrzeciwnika przeciwnik = colider.transform.parent.GetComponent <ZdrowiePrzeciwnika> ();
				if (przeciwnik == null)
					przeciwnik = colider.GetComponent <ZdrowiePrzeciwnika> ();
				if (przeciwnik != null) {
					przeciwnik.TakeDamage (damage);
				}
				if (colider.attachedRigidbody != null)
					colider.attachedRigidbody.AddExplosionForce (1000, transform.position, 2);		
			}
		}
		particles.Play ();
		boom.Play ();
		isParticles = true;
	}
}
