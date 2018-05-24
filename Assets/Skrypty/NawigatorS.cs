using UnityEngine;
using System.Collections;

public class NawigatorS : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent navMesh;
	Transform vendor;

	void Start () {
		navMesh = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		vendor = GameObject.Find ("Vendor").transform;
	}

	void Update () {
		navMesh.SetDestination (vendor.position);
		if (Vector3.Distance (transform.position, vendor.position) <= 1) {
			Destroy (gameObject);
		}
	}
}
