using UnityEngine;
using System.Collections;

public class MarbleMovement : MonoBehaviour
{
    public float ForceMultiplier = 10.0f;

    private Rigidbody rb;
    private HUD_Class ui;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();

        ui = (GameObject.FindGameObjectWithTag("UI")).GetComponent<HUD_Class>();
    }

    // Update is called once per frame
    void Update()
    {   
        // when marble falls from the board, level will be restarted
        if (transform.position.y <= -5)
        {
            ui.Fail();
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
