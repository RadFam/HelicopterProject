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
	[SerializeField]
	ButtonController buttonController;

	[SerializeField]
	GameObject helicObject;

	private int counterUp = 0;
    private int counterLeft = 0;
	private float specTime = 0.01f;
	private Quaternion initHelicRotation; 

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
		initHelicRotation = helicObject.transform.rotation;
		canMove = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
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

#if UNITY_ANDROID
		if (buttonController.UpBtn.IsPressed)
		{
			updnM = 1;
		}
		if (buttonController.DownBtn.IsPressed)
		{
			updnM = -1;
		}
		if (buttonController.LeftBtn.IsPressed)
		{
			rotM = -1;
		}
		if (buttonController.RightBtn.IsPressed)
		{
			rotM = 1;
		}
		if (buttonController.MoveBtn.IsPressed)
		{
			frwdM = 1;
		}
#endif


		if (canMove)
		{

			if (specTime >= 0.0f)
			{
				specTime = 0.0f;
				// up self-rotation
				if ((updnM == 1) && (counterUp > -5))
                {
                    helicObject.transform.Rotate(-1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp--;
                }
				// down self-rotation
                if ((updnM == -1) && (counterUp < 5))
                {
                    helicObject.transform.Rotate(1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp++;
                }
				// left self-rotation
                if ((rotM == 1) && (counterLeft > -5))
                {
                    helicObject.transform.Rotate(0.0f, 0.0f, -1.6f, Space.Self);
                    counterLeft--;
                }
				// right self-rotation
                if ((rotM == -1) && (counterLeft < 5))
                {
                    helicObject.transform.Rotate(0.0f, 0.0f, 1.6f, Space.Self);
                    counterLeft++;
                }
				// normalize up/down orientation
                if ((updnM == 0) && (counterUp != 0))
                {
                    int sign = counterUp / Mathf.Abs(counterUp);
                    helicObject.transform.Rotate(sign * (-1.6f), 0.0f, 0.0f, Space.Self);
                    counterUp = counterUp - sign;
                }
				// normalize left/rigth orientation
                if ((rotM == 0) && (counterLeft != 0))
                {
                    int sign = counterLeft / Mathf.Abs(counterLeft);
                    helicObject.transform.Rotate(0.0f, 0.0f, sign * (-1.6f), Space.Self);
                    counterLeft = counterLeft - sign;
                }
				if ((updnM == 0) && (rotM == 0) && (counterLeft == 0) && (counterUp == 0))
                {
                    helicObject.transform.localRotation = initHelicRotation;
                }
			}
			specTime += Time.deltaTime;


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
