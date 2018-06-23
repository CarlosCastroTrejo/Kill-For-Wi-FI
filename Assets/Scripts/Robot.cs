using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Robot : NetworkBehaviour {

	public Animator animation;

	public FixedJoystick MoveJoystick;
	public FixedButton JumpButton;
	public FixedTouchField TouchField;
	public FixedButton ShootButton;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;


	private Vector2 Arriba=new Vector2(0.0f,1.0f);
	private Vector2 Derecha=new Vector2(1.0f,0.0f);
	private Vector2 Izquierda=new Vector2(-1.0f,0.0f);
	private Vector2 Abajo=new Vector2(0.0f,-1.0f);



	// Use this for initialization
	void Start () 
	{
		animation = GetComponent<Animator> ();
		
	}

	// Update is called once per frame
	void Update () 
	{
		if (isLocalPlayer) {

			var fps = GetComponent<RigidbodyFirstPersonController> ();

			fps.RunAxis = MoveJoystick.inputVector; 
			fps.JumpAxis = JumpButton.Pressed; 
			fps.mouseLook.LookAxis = TouchField.TouchDist;





			if ((int)(fps.RunAxis.x * 100) >= -5 && (int)(fps.RunAxis.x * 100) <= 5 && (int)(fps.RunAxis.y * 100) > 90) {
				animation.Play ("walk_shoot_ar", -1, 0f);
			} else if ((int)(fps.RunAxis.x * 100) >= -5 && (int)(fps.RunAxis.x * 100) <= 5 && (int)(fps.RunAxis.y * 100) < -90) {
				animation.Play ("walkBack_shoot_ar", -1, 0f);
			} else if ((int)(fps.RunAxis.y * 100) >= -5 && (int)(fps.RunAxis.y * 100) <= 5 && (int)(fps.RunAxis.x * 100) > 90) {
				animation.Play ("walkRight_shoot_ar", -1, 0f);
			} else if ((int)(fps.RunAxis.y * 100) >= -5 && (int)(fps.RunAxis.y * 100) <= 5 && (int)(fps.RunAxis.x * 100) < -90) {
				animation.Play ("walkLeft_shoot_ar", -1, 0f);
			} else if (ShootButton.Pressed) {
				animation.Play ("shoot_single_ar", -1, 0f);
				CmdFire ();
			}
		} else 
		{
			return;
		}


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
