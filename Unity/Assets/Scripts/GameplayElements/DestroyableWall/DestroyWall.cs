using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour {

	public float withstandingForce = 4;


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Marble") {
			if (collision.impactForceSum.x * collision.rigidbody.mass > withstandingForce
				| collision.impactForceSum.y * collision.rigidbody.mass > withstandingForce
				| collision.impactForceSum.z * collision.rigidbody.mass > withstandingForce
			
				|collision.impactForceSum.y * collision.rigidbody.mass < -withstandingForce	
				|collision.impactForceSum.x * collision.rigidbody.mass < -withstandingForce
				|collision.impactForceSum.z * collision.rigidbody.mass < -withstandingForce)
            {
                this.GetComponent<Renderer> ().enabled = false;
				this.GetComponent<Collider> ().enabled = false;			
			}
		}
	}
}
