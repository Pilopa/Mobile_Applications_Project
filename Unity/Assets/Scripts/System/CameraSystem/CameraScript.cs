using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {

    ///<summary> MarbleCount, not needed at the moment </summary>
    public short MarbleCount { get; private set; }
    /// <summary> Stores all marbles in the level. </summary> Used for setting position of the camera.
    private GameObject[] marbles;
    private GameObject board;

    // Use this for initialization
	void Start () {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        MarbleCount = (short) marbles.Length;
        board = GameObject.Find("Board");
	}
	
	// Update is called once per frame
	void Update () {
        // Camera follows the marble with static distance
        if (MarbleCount == 1)
        {
            Vector3 marblePos = marbles[0].transform.position;
            Vector3 up = board.transform.up.normalized;
            transform.position = marblePos + (up * 15);
        }
        
        // TODO: Camera behavior with more than 1 marble
    }
}
