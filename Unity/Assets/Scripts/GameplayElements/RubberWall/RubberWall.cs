using UnityEngine;
using System.Collections;

public class RubberWall : MonoBehaviour {

	public int speed = 500;

	// Use this for initialization
	void Start () {		
		gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
	}	


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Marble") {
			other.rigidbody.AddForce (-transform.forward * speed);
		}

	}
}
