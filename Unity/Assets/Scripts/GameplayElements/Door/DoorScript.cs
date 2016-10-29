using UnityEngine;
using Assets.Scripts.GameplayElements;

public class DoorScript : MonoBehaviour, ITriggerable {
   
    public float Speed; //{ get; set; }
    public bool IsTriggered { get; private set; }
    /// <summary>
    /// Defines the original position of the door, so it can move back there.
    /// </summary>
    public GameObject StartPosition;
    /// <summary>
    /// Defines the destination of the opening translation.
    /// </summary>
    public GameObject Destination;

	// Use this for initialization
	void Start ()
    {
        IsTriggered = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (IsTriggered)
        {
            // Getting the direction in which the door is moved
            Vector3 direction = Vector3.Normalize(Destination.transform.position - transform.position);
            // Door opens
            if (Vector3.Distance(transform.position, Destination.transform.position) >= 0.01)
            {
                transform.position = transform.position + direction * Speed * Time.deltaTime;
            }
        }
        else
        {
            // Getting the direction in which the door is moved
            Vector3 direction = Vector3.Normalize(StartPosition.transform.position - transform.position);
            //Door closes
            if (Vector3.Distance(transform.position, StartPosition.transform.position) >= 0.01)
            {
                transform.position = transform.position + direction * Speed * Time.deltaTime;
            }
        }
    }


    public void TriggerEnter(Collider col)
    {
        IsTriggered = true;
    }

    public void TriggerExit(Collider col)
    {
        IsTriggered = false;
    }
}
