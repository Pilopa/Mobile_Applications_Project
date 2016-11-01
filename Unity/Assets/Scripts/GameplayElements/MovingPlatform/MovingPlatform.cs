using Assets.Scripts.GameplayElements;
using UnityEngine;


class MovingPlatform : MonoBehaviour, ITriggerable
{
    public GameObject StartPoint, EndPoint;
    /// <summary>
    /// determines whether platform gets triggered or moves all time
    /// </summary>
    public bool IsActivated = false;
    public float Speed = 1;

    private Vector3 start, end;
    private Vector3 destination;
    //for trigger handling
    private bool isActivated;

    void Start()
    {
        start = StartPoint.transform.position;
        end = EndPoint.transform.position;
        destination = end;
        isActivated = IsActivated;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            // Getting the direction in which the platform is moved
            Vector3 direction = Vector3.Normalize(destination - transform.position);
            if (Vector3.Distance(transform.position, destination) >= 0.01)
            {
                transform.position = transform.position + direction * Speed * Time.deltaTime;
            }
            else
            {
                if (destination == start) destination = end;
                else if (destination == end) destination = start;
            }
        }
    }

    public void TriggerEnter(Collider col)
    {
        isActivated = true;
    }

    public void TriggerExit(Collider col)
    {
        isActivated = false;
    }
}

