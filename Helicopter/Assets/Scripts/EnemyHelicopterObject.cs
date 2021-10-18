using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopterObject : MonoBehaviour 
{
	float updnM = 0;
	float rotM = 0;
	int counterUp = 0;
    int counterLeft = 0;
	float specTime = 0.01f;
	Quaternion initHelicRotation;

	public int UpdnM
	{
		set {updnM = value;}
	}
	public int RotM
	{
		set {rotM = value;}
	}
	// Use this for initialization
	void Start () 
	{
		initHelicRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (specTime >= 0.0f)
			{
				specTime = 0.0f;
				// up self-rotation
				if ((updnM == 1) && (counterUp > -5))
                {
                    transform.Rotate(-1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp--;
                }
				// down self-rotation
                if ((updnM == -1) && (counterUp < 5))
                {
                    transform.Rotate(1.6f, 0.0f, 0.0f, Space.Self);
                    counterUp++;
                }
				// left self-rotation
                if ((rotM == 1) && (counterLeft > -5))
                {
                    transform.Rotate(0.0f, 0.0f, -1.6f, Space.Self);
                    counterLeft--;
                }
				// right self-rotation
                if ((rotM == -1) && (counterLeft < 5))
                {
                    transform.Rotate(0.0f, 0.0f, 1.6f, Space.Self);
                    counterLeft++;
                }
				// normalize up/down orientation
                if ((updnM == 0) && (counterUp != 0))
                {
                    int sign = counterUp / Mathf.Abs(counterUp);
                    transform.Rotate(sign * (-1.6f), 0.0f, 0.0f, Space.Self);
                    counterUp = counterUp - sign;
                }
				// normalize left/rigth orientation
                if ((rotM == 0) && (counterLeft != 0))
                {
                    int sign = counterLeft / Mathf.Abs(counterLeft);
                    transform.Rotate(0.0f, 0.0f, sign * (-1.6f), Space.Self);
                    counterLeft = counterLeft - sign;
                }
				if ((updnM == 0) && (rotM == 0) && (counterLeft == 0) && (counterUp == 0))
                {
                    transform.localRotation = initHelicRotation;
                }
			}
			specTime += Time.deltaTime;	
	}
}
