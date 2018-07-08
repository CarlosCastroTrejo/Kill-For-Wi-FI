using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Billboard : MonoBehaviour
{
	public Camera cam;
	void Update () 
	{
		transform.LookAt(cam.transform);
	}
}
