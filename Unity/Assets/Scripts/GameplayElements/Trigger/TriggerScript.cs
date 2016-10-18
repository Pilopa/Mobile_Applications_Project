using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
    /// <summary>
    /// The door which is opened and closed by this trigger
    /// </summary>
    public GameObject Door;
    /// <summary>
    /// Shows if marble is on the trigger
    /// </summary>
    private bool isActivated = false;
    private DoorScript doorScript;

	// Use this for initialization
	void Start () {
        doorScript = Door.GetComponent<DoorScript>();
        Debug.Log("Start Trigger");
	}
	
	// Update is called once per frame
	void Update () {
        if (isActivated)
        {
            if(Vector3.Distance(Door.transform.position, doorScript.StartPosition) <= doorScript.MovingDistance)
                Door.transform.position = Door.transform.position - doorScript.MovingDirection * doorScript.Speed * Time.deltaTime;
        }
        else
        {
            /*
            if (Vector3.Distance(Door.transform.position, doorScript.StartPosition) >= 0.1)
                Door.transform.position = Door.transform.position + doorScript.MovingDirection * doorScript.Speed * Time.deltaTime;
            */
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Marble")
        {
            isActivated = true;
            Debug.Log("Enter");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Marble")
        {
            isActivated = false;
            Debug.Log("Exit");
        }
    }
}

