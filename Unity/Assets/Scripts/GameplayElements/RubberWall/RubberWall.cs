﻿using UnityEngine;
using System.Collections;

public class RubberWall : MonoBehaviour {

	public int speed = 500;


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Marble") {
			collision.rigidbody.AddForce (-transform.forward * speed);
		}

	}
}
