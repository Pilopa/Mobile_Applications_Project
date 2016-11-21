using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour {

	public float withstandingForce = 2;

	// Use this for initialization
	void Start () {			
		gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Marble") {
			if (other.impactForceSum.x * other.rigidbody.mass > withstandingForce
				| other.impactForceSum.z * other.rigidbody.mass > withstandingForce)
			{
				this.GetComponent<Renderer> ().enabled = false;
				this.GetComponent<Collider> ().enabled = false;			
			}
		}

	}
}
