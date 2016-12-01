using System;
using UnityEngine;
using System.Collections;

public class Razorblades : MonoBehaviour
{

    private GameObject[] marbles;
    private Collider col;
    private HUD_Class ui;

    // Use this for initialization
    void Start()
    {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        col = GetComponent<Collider>();
        ui = (GameObject.FindGameObjectWithTag("UI")).GetComponent<HUD_Class>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        // transform.Rotate(0, Time.deltaTime*90f, 0, Space.Self);
        //transform.Rotate(0, Time.deltaTime, 0, Space.World);

        transform.Rotate(new Vector3(Time.deltaTime * 180, 0, 0));

        if (col.tag == "Marble")
        {
            ui.Fail();
            return;
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Marble")
        {
            ui.Fail();
            return;
        }
    }


}