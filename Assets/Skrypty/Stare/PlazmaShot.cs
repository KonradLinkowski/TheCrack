using UnityEngine;
using System.Collections;

public class PlazmaShot : MonoBehaviour {

	public float force;
	Rigidbody rb;

	void Start() {
		rb = GetComponent <Rigidbody> ();
		rb.AddForce (Vector3.forward * force, ForceMode.VelocityChange);
	}
}