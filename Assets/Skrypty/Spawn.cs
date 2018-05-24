using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject enemy;

	public const int numberOfWaves = 10;

	public Transform[] spawnPoint = new Transform[9];

	public GameObject[] enemies = new GameObject [3];

	public float minimumTime = 1;

	public int[] numberOfEnemies = new int[numberOfWaves];

	public Transform gracz;

	public float minimalnaOdleglosc;

	bool enemiesDead;
	
	public int enemiesRemaining;

	RaycastHit hit;

	public int wave = 0;
	int randomEnemy;
	int randomPosition;

	float timer = 0;

	RaycastHit test;
	public GameObject tester;

	float testTimer;
	public float testTime;

	public float dlugoscPrzerwy = 20;
	public float pauseTimer;
	bool final;

	void Start () {
		enemiesRemaining = numberOfEnemies [0];
		pauseTimer = dlugoscPrzerwy;
		testTimer = testTime;

	}

	void Update () {
		timer += Time.deltaTime;
		if (wave != 9) {
			if (timer >= minimumTime) {
				randomPosition = Random.Range (0, spawnPoint.Length);
				if (numberOfEnemies [wave] > 0 && Vector3.Distance (spawnPoint [randomPosition].position, gracz.position) >= minimalnaOdleglosc
				    && Physics.Raycast (spawnPoint [randomPosition].position, gracz.position, out hit, 100)) {
					if (!hit.transform.gameObject.CompareTag ("Player")) {
						randomEnemy = Random.Range (0, enemies.Length);
						Instantiate (enemies [randomEnemy], spawnPoint [randomPosition].position, spawnPoint [randomPosition].rotation);
						numberOfEnemies [wave]--;
						timer = 0;
					}
				}
			}

			if (numberOfEnemies [wave] == 0 && enemiesRemaining == 0) {
				pauseTimer -= Time.deltaTime;
				testTimer += Time.deltaTime;
				if (testTimer >= testTime) {
					testTimer = 0;
					Instantiate (tester, gracz.position, gracz.rotation);
				}
				if (pauseTimer <= 0) {
					wave++;
					enemiesRemaining = numberOfEnemies [wave];
					pauseTimer = dlugoscPrzerwy;
				}
			}
		} else {
			Instantiate (enemy, new Vector3 (0, 0.5f, 0), Quaternion.identity);
			Destroy (this);
		}
	}
}