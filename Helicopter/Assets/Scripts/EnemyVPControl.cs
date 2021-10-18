using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVPControl : MonoBehaviour 
{
	// Use this for initialization
	public void SetForwardOrientation(Vector3 orient)
	{
		transform.forward = orient;
	}
}
