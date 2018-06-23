using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController3 : NetworkBehaviour 
{
	
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public FixedJoystick MoveJoystick;
	public FixedButton JumpButton;
	public FixedTouchField TouchField;
	public FixedButton ShootButton;


	// Update is called once per frame
	void Update () 
	{
		
		if (!isLocalPlayer) 
		{
			return;
		}	
		/*var fps = GetComponent<RigidbodyFirstPersonController> ();

		fps.RunAxis = MoveJoystick.inputVector; 
		fps.JumpAxis = JumpButton.Pressed; 
		fps.mouseLook.LookAxis = TouchField.TouchDist;*/

		transform.Translate (MoveJoystick.inputVector.x*.2f,0,MoveJoystick.inputVector.y*.2f);

			
		if (ShootButton.Pressed) 
		{
			CmdFire ();
		}
		if (JumpButton.Pressed) 
		{
			transform.Translate (0, 2 * Time.deltaTime * 3.0f, 0);
		}
	}


	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire()
	{
		//Create the Bullet from the Bullet Prefab
		var bullet=(GameObject)Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);

		//Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		//Destroy bullet after 2 seconds 
		Destroy(bullet,2.0f);

	}
}
