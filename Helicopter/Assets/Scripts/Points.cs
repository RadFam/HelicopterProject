using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
	}
}
