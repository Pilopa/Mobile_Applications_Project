using UnityEngine;
using System.Collections;

public class MarbleMovement : MonoBehaviour
{

    private Vector3 initialOrientation;
    private Vector3 deviceRotation;
    private Vector3 velocity;
    private float maxVeloctiy = 1.0f;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();

        initialOrientation = Input.gyro.attitude.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gyroRot = Input.gyro.attitude.eulerAngles;
        Vector3 rot = gyroRot - initialOrientation;

        if (rot.x > 180 && rot.x < 360)
        {
            if (rb.velocity.x <= maxVeloctiy)
                rb.velocity = rb.velocity + new Vector3(0, 0, (360 - rot.x) / 100);
        }
        if (rot.x > 0 && rot.x < 180)
        {
            if (rb.velocity.x >= (-maxVeloctiy))
                rb.velocity = rb.velocity + new Vector3(0, 0, -(rot.x / 100));
        }

        if (rot.y > 180 && rot.y < 358)
        {
            if (rb.velocity.y <= maxVeloctiy)
                rb.velocity = rb.velocity + new Vector3(-((360 - rot.y) / 100), 0, 0);
        }
        if (rot.y > 2 && rot.y < 180)
        {
            if (rb.velocity.y >= (-maxVeloctiy))
                rb.velocity = rb.velocity + new Vector3(rot.y / 100, 0, 0);
        }

    }
}
