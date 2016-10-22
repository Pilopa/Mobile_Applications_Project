using UnityEngine;

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
        Vector3 pos = Vector3.zero;
        if (MarbleCount == 1)
        {
            pos = marbles[0].transform.position;
        }else
        {

        }
        Vector3 up = board.transform.up.normalized;
        transform.position = pos + (up * 15);

        // TODO: Camera behavior with more than 1 marble
    }
}
