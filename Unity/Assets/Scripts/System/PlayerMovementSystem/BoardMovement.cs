using UnityEngine;
using System.Collections;

public class BoardMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // For Editor use
<<<<<<< HEAD
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
=======
		#if (UNITY_EDITOR)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
>>>>>>> 6f47717306537c775f516331a1ac662c472c9a4c

        //if(h != 0)
        //{
        //    transform.Rotate(transform.forward, 0.5f * h);
        //}
        //if(v != 0)
        //{
        //    transform.Rotate(transform.right, 0.5f * v);
        //}

<<<<<<< HEAD
        //// For mobile devices
        //float x = Mathf.Clamp((Input.acceleration.x * 1f / 3f), -30f, 30f); // so that 90 deg of mobile rotation equals 30 deg of board rotation and can't get higher than 30.
        //float z = Mathf.Clamp(Input.acceleration.y * 1f / 3f, -30f, 30f); // y-axis on phone equals the z-axis of the board

        //transform.Rotate(new Vector3(x, 0, z));
=======
        // For mobile devices
		#elif (UNITY_ANDROID)
        float x = Input.acceleration.x;
        float z = Input.acceleration.y; // y-axis on phone equals the z-axis of the board
        transform.Rotate(new Vector3(x, 0, z));
		#endif
>>>>>>> 6f47717306537c775f516331a1ac662c472c9a4c
	}
}
