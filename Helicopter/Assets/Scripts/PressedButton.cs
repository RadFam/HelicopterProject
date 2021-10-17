using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButton : MonoBehaviour 
{
	bool isPressed;
	
	public bool IsPressed
	{
		get {return isPressed;}
	}
	void Start () 
	{
		isPressed = false;	
	}
	
	public void OnPointerDown()
	{
		isPressed = true;
	}

	public void OnPointerUp()
	{
		isPressed = false;
	}
}
