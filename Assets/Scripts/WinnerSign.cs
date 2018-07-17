using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WinnerSign : NetworkBehaviour {

	public Canvas controles;
	public Canvas winner;

	[SyncVar]
	public int playersConnected;


	void Start()
	{
		winner.enabled=false;
	}

	
	// Update is called once per frame
	void Update () 
	{
		
		
		playersConnected = NetworkServer.connections.Count; // Counter to know how many players are on the server




		if (NetworkServer.connections.Count==1) 
		{
			//controles.enabled = false;
			//winner.enabled= true;
		}
		
	}
}
