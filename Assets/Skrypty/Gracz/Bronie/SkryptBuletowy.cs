using UnityEngine;
using System.Collections;

public class SkryptBuletowy : MonoBehaviour {

	Vector3 prosto = Vector3.forward;
	public float speed = 1;
	float timer;
	public float deathTime = 10;
	bool koniec;
	public int damage;

	void Start () {
		prosto = transform.InverseTransformDirection (transform.forward);
	}

	void Update () {
		timer += Time.deltaTime;
		//Vector3 kierunek = new Vector3 (0, 0, speed);
		if (!koniec)
			transform.Translate (prosto * speed * Time.deltaTime);
		if (timer >= deathTime)
			Destroy (gameObject);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer != 8)
			return;
		GraczFPS gracz = other.GetComponent <GraczFPS> ();
		if (gracz != null)
			return;
		ZdrowiePrzeciwnika enemyHealth = other.GetComponentInParent <ZdrowiePrzeciwnika> ();
		if (enemyHealth != null) {
			enemyHealth.TakeDamage (damage);
			transform.SetParent (other.gameObject.transform);
		}
		GetComponent<Collider>().gameObject.GetComponent <Collider> ().enabled = false;
		koniec = true;
	}
}
