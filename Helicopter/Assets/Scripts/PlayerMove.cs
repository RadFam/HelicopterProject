using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour 
{
	[SerializeField]
	float forwardSpeed;

	[SerializeField]
	float verticalSpeed;

	[SerializeField]
	float rotationSpeed;

	[SerializeField]
	Rigidbody myRigid;

	Vector3 fullVelocity;
	float frwdM = 0;
	float updnM = 0;
	float rotM = 0;
	Vector3 eulerAngle;
	Quaternion deltaRot;
	public bool canMove;

	// Use this for initialization
	void Start () 
	{
		fullVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		eulerAngle = new Vector3(0, rotationSpeed, 0);
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckForPress();
	}

	void CheckForPress()
	{
		frwdM = 0;
		updnM = 0;
		rotM = 0;

		if (Input.GetKey(KeyCode.Space))
		{
			frwdM = 1;
		}

		updnM = Input.GetAxisRaw("Vertical");
		rotM = Input.GetAxisRaw("Horizontal");

		if (canMove)
		{
			deltaRot = Quaternion.Euler(eulerAngle*rotM*Time.deltaTime);

			myRigid.velocity = forwardSpeed*frwdM*transform.forward + verticalSpeed*updnM*transform.up;
			myRigid.MoveRotation(myRigid.rotation*deltaRot);
		}
	}

	public void StopMove()
	{
		canMove = false;
		myRigid.velocity = new Vector3(0,0,0);
		myRigid.rotation = Quaternion.identity;
		myRigid.isKinematic = true;
	}
}
