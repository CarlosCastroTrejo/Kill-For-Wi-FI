using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class World : MonoBehaviour {

	// Use this for initialization
	public GameObject level1;
	public GameObject level2;


	float theTime;
	public float speed = 1;



	// Update is called once per frame
	void Update () 
	{
		
		theTime += Time.deltaTime * speed;
		string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
		string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
		string seconds = (theTime % 60).ToString("00");

		if (minutes=="02" && seconds=="25") 
		{
			level1.SetActive (false);
			level2.SetActive (true);
		}
		
	}
}
