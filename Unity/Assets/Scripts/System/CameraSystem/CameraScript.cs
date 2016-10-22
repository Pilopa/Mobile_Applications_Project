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
        Vector3 posC = Vector3.zero;
        float cameraDistance = 15;
        if (MarbleCount == 1)
        {
            
        }else if(MarbleCount == 2)
        {
            Vector3 pos1 = marbles[0].transform.position;
            Vector3 pos2 = marbles[1].transform.position;
            posC = ((pos1 - pos2) / 2) + pos2; // Calculation of the center of the line connecting both marbles
            // TODO : cameraDstance need to be changed when marbles get to far away.
            
        }
        Vector3 up = board.transform.up.normalized;
        transform.position = posC + (up * cameraDistance);

        // TODO : Camera behavior with more than 2 marbles
    }
}
