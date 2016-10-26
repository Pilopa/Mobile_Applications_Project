using UnityEngine;
using System.Collections;

public class MarbleMovement : MonoBehaviour {

    private Vector3 initialOrientation;
    private Vector3 deviceRotation;
    private Vector3 velocity;
    private float maxVeloctiy = 1.0f;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();
        
        initialOrientation = Input.gyro.attitude.eulerAngles;
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

        Vector3 gyroRot = Input.gyro.attitude.eulerAngles;
        Vector3 rot = gyroRot - initialOrientation;

        if (rot.x > 330 && rot.x < 358)
        {
            if (rb.velocity.x <= maxVeloctiy)
                rb.velocity = rb.velocity + new Vector3(0, 0, rot.x / 100);
        }
        if (rot.x > 2 && rot.x < 30)
        {
            if (rb.velocity.x >= (- maxVeloctiy))
                rb.velocity = rb.velocity + new Vector3(0, 0, - (rot.x / 100));
        }

        if (rot.y > 330 && rot.y < 358)
        {
            if (rb.velocity.y <= maxVeloctiy)
                rb.velocity = rb.velocity + new Vector3(-(rot.x / 100), 0, 0);
        }
        if (rot.y > 2 && rot.y < 30)
        {
            if (rb.velocity.y >= (- maxVeloctiy))
                rb.velocity = rb.velocity + new Vector3(rot.x /100, 0, 0);
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
