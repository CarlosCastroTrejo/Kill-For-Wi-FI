using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController3 : NetworkBehaviour 
{
	
	public GameObject bulletPrefab;
    public GameObject RightWheel;
    public GameObject LeftWheel;



	public Transform bulletSpawn;
  
	public Camera cam;
	public Canvas controles;
	public FixedJoystick MoveJoystick;
	public FixedButton JumpButton;
	public FixedTouchField TouchField;
	public FixedButton ShootButton;


   

	// Update is called once per frame
	void Update () 
	{
        if (!isLocalPlayer) 
        {
          
			cam.enabled = false;
			controles.enabled = false;
			return;
        } 
        //Move all the Object
		transform.Translate(MoveJoystick.inputVector.x * .4f, 0, MoveJoystick.inputVector.y * .4f);
		transform.Rotate(0,TouchField.TouchDist.x * .08f,0);
	
        //Move the wheels in a vertical way
        if (MoveJoystick.inputVector.y == 0)
        { 
        }
        else if(MoveJoystick.inputVector.y>0)
        {
            RightWheel.transform.Rotate(-10, 0, 0);
            LeftWheel.transform.Rotate(10, 0, 0);
        }else if(MoveJoystick.inputVector.y<0)
        {
            RightWheel.transform.Rotate(10, 0, 0);
            LeftWheel.transform.Rotate(-10, 0, 0);
        }else{}

			

        //Jump
		if (JumpButton.Pressed) 
		{
			transform.Translate (0, 5 * Time.deltaTime * 3.0f, 0);
		}

        //Fire
        if (ShootButton.Pressed)
        {
            CmdFire();
        }

	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
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
