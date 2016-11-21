using UnityEngine;
using System.Collections;

public class MarbleMovement : MonoBehaviour
{
    public float ForceMultiplier = 10.0f;

    //private Vector3 initialOrientation;
    //private Vector3 deviceRotation;
    //private Vector3 velocity;
    private Rigidbody rb;
    private HUD_Class ui;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();

        //initialOrientation = Input.gyro.attitude.eulerAngles;

        ui = (GameObject.FindGameObjectWithTag("UI")).GetComponent<HUD_Class>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 gyroRot = Input.gyro.attitude.eulerAngles;
        //Vector3 rot = gyroRot - initialOrientation;

        //if (rot.x > 180 && rot.x < 360)
        //{
        //    if (rb.velocity.x <= maxVeloctiy)
        //        rb.velocity = rb.velocity + new Vector3(0, 0, (360 - rot.x) / 100);
        //}
        //if (rot.x > 0 && rot.x < 180)
        //{
        //    if (rb.velocity.x >= (-maxVeloctiy))
        //        rb.velocity = rb.velocity + new Vector3(0, 0, -(rot.x / 100));
        //}

        //if (rot.y > 180 && rot.y < 358)
        //{
        //    if (rb.velocity.y <= maxVeloctiy)
        //        rb.velocity = rb.velocity + new Vector3(-((360 - rot.y) / 100), 0, 0);
        //}
        //if (rot.y > 2 && rot.y < 180)
        //{
        //    if (rb.velocity.y >= (-maxVeloctiy))
        //        rb.velocity = rb.velocity + new Vector3(rot.y / 100, 0, 0);
        //}
        
        // when marble falls from the board, level will be restarted
        if (transform.position.y <= -5)
        {
            ui.RestartGame();
            return;
        }
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        // mobile
#if UNITY_ANDROID
        moveHorizontal = Input.acceleration.x;
        moveVertical = Input.acceleration.y;
#endif
        //editor
#if UNITY_EDITOR
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
#endif

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * ForceMultiplier);
    }
}
