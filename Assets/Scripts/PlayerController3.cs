using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController3 : NetworkBehaviour 
{
	// Initilization of the objects 
	public GameObject bulletPrefab;
    public GameObject RightWheel;
    public GameObject LeftWheel;
	public GameObject Rifle;
	public GameObject Crosshair;
	public ParticleSystem muzzleflash; 


	public Transform bulletSpawn;
  
	public Camera cam;
	public Canvas controles;
	public AudioListener audio2;
	public FixedJoystick MoveJoystick;
	public FixedButton JumpButton;
	public FixedTouchField TouchField;
	public FixedButton ShootButton;

	private int shootCounter=0;
	private int jumpCounter=0;




   

	// Update is called once per frame
	void Update ()
	{
		
		if (!isLocalPlayer) {
          
			cam.enabled = false;
			controles.enabled = false;
			audio2.enabled = false;
			return;
		} 

		shootCounter++; // Counter so the player won't shoot continously
		jumpCounter++; // Counter so the player won't jump continously


        // Move all the object	
		transform.Translate(MoveJoystick.inputVector.x * .4f, 0, MoveJoystick.inputVector.y * .4f);
		// Rotate all the object
		transform.Rotate(0,TouchField.TouchDist.x * .08f,0);
		Rifle.transform.Rotate (TouchField.TouchDist.y * 0.08f, 0, 0);
		Crosshair.transform.Translate (0,-TouchField.TouchDist.y * 2.0f, 0);
	
        // Rotate the wheels
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
		if (JumpButton.Pressed && jumpCounter>=7) 
		{
			transform.Translate (0, 8 * Time.deltaTime * 3.0f, 0);
			jumpCounter = 0;
		}

        //Fire
		if (ShootButton.Pressed && shootCounter>=7)
        {
			muzzleflash.Play ();
			CmdFire();
			shootCounter = 0;
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
		RaycastHit hit;

		//Create the Bullet from the Bullet Prefab
		var bullet=(GameObject)Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);

		//Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 7;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		//Destroy bullet after 2 seconds 
		Destroy (bullet,2.0f);


	}
}
