using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotate : MonoBehaviour 
{
	[SerializeField]
	float rotationSpeed;
	Vector3 eulerAngle;
	Quaternion deltaRot;
	Quaternion initRot;
	float timer;
	// Use this for initialization
	void Start () 
	{
		eulerAngle = new Vector3(0, rotationSpeed, 0);
		initRot = transform.rotation;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//timer += Time.deltaTime;
		//deltaRot = Quaternion.Euler(eulerAngle * timer);
		//transform.rotation = initRot*deltaRot;
		transform.RotateAround(transform.position, transform.up, rotationSpeed*Time.deltaTime);
	}
}
