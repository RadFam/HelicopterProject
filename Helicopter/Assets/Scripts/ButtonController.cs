using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour 
{

	[SerializeField]
	PressedButton upBtn;
	[SerializeField]
	PressedButton downBtn;
	[SerializeField]
	PressedButton leftBtn;
	[SerializeField]
	PressedButton rightBtn;
	[SerializeField]
	PressedButton moveBtn;

	public PressedButton UpBtn
	{
		get {return upBtn;}
	}
	public PressedButton DownBtn
	{
		get {return downBtn;}
	}
	public PressedButton LeftBtn
	{
		get {return leftBtn;}
	}
	public PressedButton RightBtn
	{
		get {return rightBtn;}
	}
	public PressedButton MoveBtn
	{
		get {return moveBtn;}
	}
	void Start () {
		
	}
	
}
