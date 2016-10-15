using UnityEngine;
using System.Collections;

public class BoardMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // For Editor use
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0)
        {
            transform.Rotate(transform.forward, 0.5f * h);
        }
        if(v != 0)
        {
            transform.Rotate(transform.right, 0.5f * v);
        }

        // For mobile devices
        float x = Input.acceleration.x;
        float z = Input.acceleration.z;
        transform.Rotate(new Vector3(x, 0, z));

	}
}
