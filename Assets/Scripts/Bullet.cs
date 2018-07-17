using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject ImpactEffect; 


	 void OnCollisionEnter(Collision collision)
	{
		
		var hit = collision.gameObject;
		var hit2 = collision.gameObject;
		var health = hit.GetComponent<Health>();
		var health2 = hit2.GetComponent<HealthObjects> ();
		if (health  != null)
		{
			health.TakeDamage(10);
			GameObject Impact = Instantiate (ImpactEffect, gameObject.transform.position, Quaternion.LookRotation (gameObject.transform.position));
			Destroy (Impact, 1.0f);
		}
		if (health2 != null) 
		{
			health2.TakeDamage (10);
			GameObject Impact = Instantiate (ImpactEffect, gameObject.transform.position, Quaternion.LookRotation (gameObject.transform.position));
			Destroy (Impact, 1.0f);
		}

		Destroy(gameObject);

	}



}
