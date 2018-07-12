using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;



public class Clock : MonoBehaviour {

	public Text Reloj;
	float theTime;
	public float speed = 1;
	
	// Update is called once per frame
	void Update () 
	{

		theTime += Time.deltaTime * speed;
		string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
		string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
		string seconds = (theTime % 60).ToString("00");
		Reloj.text = hours + ":" + minutes + ":" + seconds;

		if (minutes == "02" && seconds == "00")
		{
			Reloj.color = Color.red;
		}
		if (minutes == "02" && seconds == "26") 
		{
			Reloj.color = Color.black;

		}

	}
}




