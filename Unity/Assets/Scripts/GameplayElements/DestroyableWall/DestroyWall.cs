using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour {

	public float withstandingForce = 4;

	void OnCollisionEnter(Collision collision)
	{		
		if (collision.gameObject.tag == "Marble") {
			if (collision.impactForceSum.x > withstandingForce
			|| collision.impactForceSum.y > withstandingForce
			|| collision.impactForceSum.z > withstandingForce
			|| collision.impactForceSum.x < -withstandingForce	
			|| collision.impactForceSum.y < -withstandingForce
			|| collision.impactForceSum.z < -withstandingForce) 
			{
               	this.GetComponent<Renderer> ().enabled = false;
				this.GetComponent<Collider> ().enabled = false;			
			}
		}
	}
}
