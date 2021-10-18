using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	PlayerMove player;

	[SerializeField]
	Rigidbody myRigid;
	
	void OnCollisionEnter(Collision other) 
	{
		Debug.Log("Player Collision: " + player.transform.position + " " + player.transform.eulerAngles + " " + other.gameObject.name);
		Debug.Log("Contact points: " + other.contacts[0].point);
		player.StopMove();
		myRigid.detectCollisions = false;
		UIControl.ui.ShowCollide();	
	}

}
