using UnityEngine;
using System.Collections;
using Konrad.Utility;
public class ZdrowiePrzeciwnika : MonoBehaviour {

	public int punkty;
	public int maxHp;
	int currentHealth;
	Spawn deathPoint;
	bool dead;
	SkryptGUI kanvas;

	void Start () {
		deathPoint = GameObject.Find ("Spawny").GetComponent <Spawn> ();
		kanvas = GameObject.Find ("Kanwas").GetComponent <SkryptGUI> ();
		currentHealth = maxHp;
	}

	public void TakeDamage (int amount) {
		currentHealth -= amount;
		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die () {
		if (dead)
			return;
		dead = true;
		deathPoint.enemiesRemaining--;
		kanvas.Scores (punkty);

		if (gameObject.name == "Motyl") {
			FinalS.SaveScores ("Punkty", kanvas.scoreNumber);
			GameObject boha = Resources.Load ("Bohaterka") as GameObject;
			boha = Instantiate (boha, transform.position, boha.transform.rotation) as GameObject;
			FinalS.TheEnd ();
		}
		Destroy (transform.parent.gameObject);
	}
}
