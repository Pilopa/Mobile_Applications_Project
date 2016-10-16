using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
    /// <summary>
    /// Declares the direction in wich the door is opened
    /// </summary>
    public Vector3 MovingDirection { get; private set; }
    public float Speed { get; private set; }
    public float MovingDistance { get; set; }

    public Vector3 StartPosition { get; private set; }

	// Use this for initialization
	void Start () {
        MovingDirection = transform.forward;
        Speed = 1;
        MovingDistance = 1;
        StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
