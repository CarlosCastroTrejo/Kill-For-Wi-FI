using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Robot : MonoBehaviour {

	public Animator animation;

	public FixedJoystick MoveJoystick;
	public FixedButton JumpButton;
	public FixedTouchField TouchField;

	// Use this for initialization
	void Start () 
	{
		animation = GetComponent<Animator> ();
		
	}

	// Update is called once per frame
	void Update () 
	{
		var fps = GetComponent<RigidbodyFirstPersonController> ();

		fps.RunAxis = MoveJoystick.inputVector; 
		fps.JumpAxis = JumpButton.Pressed; 
		fps.mouseLook.LookAxis = TouchField.TouchDist;



	}
}
