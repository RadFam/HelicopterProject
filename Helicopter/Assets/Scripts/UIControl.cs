using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour 
{

	public static UIControl ui;
	public Text collisionTxt;
	// Use this for initialization
	void Awake () 
	{
		if (ui == null)
		{
			ui = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	
	public void ShowCollide()
	{
		Debug.Log("Show collide");
		collisionTxt.gameObject.SetActive(true);
	}
}
