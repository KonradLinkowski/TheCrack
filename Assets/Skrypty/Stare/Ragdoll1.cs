using UnityEngine;
using System.Collections;

public class Ragdoll1 : MonoBehaviour {

	public Rigidbody l_Dlon;
	public Rigidbody p_Dlon;
	public Transform lDlon;
	public Transform pDlon;

	void Start () {
		Vector3 vlDlon = lDlon.TransformPoint (-0.433f, -0.507f, 1.073f);
		Vector3 vpDlon = pDlon.TransformPoint (0.429f, -0.507f, 1.073f);
		lDlon.localPosition = vlDlon;
		pDlon.localPosition = vpDlon;
		Quaternion qlDlon = Quaternion.Euler (-90, 0, 0);
		Quaternion qpDlon = Quaternion.Euler (-90, 0, 0);

		lDlon.localRotation = qlDlon;
		pDlon.localRotation = qpDlon;
		/*
		l_Dlon.MovePosition (vlDlon);
		l_Dlon.MoveRotation (Quaternion.Euler (-90, 0, 0));
		p_Dlon.MovePosition (vpDlon);
		p_Dlon.MoveRotation (Quaternion.Euler (-90, 0, 0));
		*/
	}
}
