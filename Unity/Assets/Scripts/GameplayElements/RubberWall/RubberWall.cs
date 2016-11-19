using UnityEngine;
using System.Collections;

public class RubberWall : MonoBehaviour {

	private int speed;

	// Use this for initialization
	void Start () {
		speed = 500;
		gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
	}	


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Marble") {
			other.rigidbody.AddForce (-transform.forward * speed);
		}

	}
}
