using UnityEngine;
using System.Collections;

public class MarbleMovement : MonoBehaviour {

    private Vector3 initialOrientation;
    private Vector3 deviceRotation;
    private Vector3 velocity;
    private float maxVeloctiy = 5.0f;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
       // rb = GetComponent<Rigidbody>();
        
        initialOrientation = Input.gyro.attitude.eulerAngles;
        deviceRotation = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 rotRate = Input.gyro.rotationRateUnbiased;
        //deviceRotation.x = deviceRotation.x + (Mathf.Rad2Deg * rotRate.x * Time.deltaTime);
        //deviceRotation.y = deviceRotation.y + (Mathf.Rad2Deg * rotRate.y * Time.deltaTime);
        //deviceRotation.z = deviceRotation.z + (Mathf.Rad2Deg * rotRate.z * Time.deltaTime);
        //velocity = velocity + new Vector3(deviceRotation.x, 0.0f, deviceRotation.y); // map y device axis to z world axis. Need to be negative because of landscape mode.

        //transform.position += velocity;

        //rb.AddTorque(torque);


        Vector3 rotation = Input.gyro.attitude.eulerAngles;
        deviceRotation = Input.gyro.attitude.eulerAngles - initialOrientation;

        if(rotation.x - initialOrientation.x > 330 && rotation.x - initialOrientation.x < 359)
        {
            transform.position = transform.position + new Vector3(0, 0, -1);
        }
        if (rotation.x - initialOrientation.x > 1 && rotation.x - initialOrientation.x < 30)
        {
            transform.position = transform.position + new Vector3(0, 0, 1);
        }

        if (rotation.y - initialOrientation.y > 330 && rotation.y - initialOrientation.y < 359)
        {
            transform.position = transform.position + new Vector3(1, 0, 0);
        }
        if (rotation.y - initialOrientation.y > 1 && rotation.y - initialOrientation.y < 30)
        {
            transform.position = transform.position + new Vector3(-1, 0, 0);
        }

    }

    void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 500, 100), deviceRotation.ToString());
        GUI.TextField(new Rect(0, 150, 500, 100), initialOrientation.ToString());
        GUI.TextField(new Rect(0, 300, 500, 100), transform.position.ToString());
        GUI.TextField(new Rect(0, 400, 500, 100), Input.gyro.attitude.eulerAngles.ToString());
    }
}
