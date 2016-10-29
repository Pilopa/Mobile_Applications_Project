using System;
using UnityEngine;

class Teleporter : MonoBehaviour
{
    /// <summary>
    ///  Destination of the teleportation. This can be another teleporter
    /// </summary>
    public GameObject Destination;
    /// <summary>
    /// Time until marble is teleported
    /// </summary>
    public float TimeToTeleport = 3;

    private Vector3 destination;
    private float time;

    void Start()
    {
        destination = Destination.transform.position;
        time = 0.0f;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Marble")
        {
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Marble")
        {
            time += Time.deltaTime;
            if (time >= TimeToTeleport)
            {
                col.transform.position = destination;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {        
        time = 0.0f;
    }
}
