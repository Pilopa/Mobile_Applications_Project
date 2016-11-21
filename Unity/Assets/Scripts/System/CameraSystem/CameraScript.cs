using UnityEngine;

public class CameraScript : MonoBehaviour {

    ///<summary> MarbleCount, not needed at the moment </summary>
    public short MarbleCount { get; private set; }
    /// <summary> Stores all marbles in the level. </summary> Used for setting position of the camera.
    public float minHeight = 15.0f;

    private GameObject[] marbles;
    private GameObject board;
    private float sqrtOf3;

    // Use this for initialization
	void Start () {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        MarbleCount = (short) marbles.Length;
        board = GameObject.Find("Board");

        sqrtOf3 = Mathf.Sqrt(3.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 posC = Vector3.zero;
        //float cameraDistance = 20.0f;
        //if (MarbleCount == 1)
        //{

        //}else if(MarbleCount == 2)
        //{
        //    Vector3 pos1 = marbles[0].transform.position;
        //    Vector3 pos2 = marbles[1].transform.position;
        //    posC = ((pos1 - pos2) / 2) + pos2; // Calculation of the center of the line connecting both marbles
        //    // TODO : cameraDstance need to be changed when marbles get to far away.

        //}
        //Vector3 up = board.transform.up.normalized;
        //transform.position = posC + (up * cameraDistance);

        // set camera by calculating average position of all marbles
        Vector3 sum = marbles[0].transform.position;
        for (int i = 1; i < marbles.Length; i++)
        {
            sum += marbles[i].transform.position;
        }
        sum = sum / marbles.Length;
        transform.position = new Vector3(sum.x, 0, sum.z); // y needs to be 0 in order to calculate the height

        // set camera distance(Y-Axis)
        // get biggest distance from camera to marble
        float maxDis = 0.0f;
        for (int i = 1; i < marbles.Length; i++)
        {
            float distance = Vector3.Distance(marbles[i].transform.position, transform.position);
            if (distance > maxDis)
                maxDis = distance;            
        }
        // calculate height of camera. Works only with a FOV of 60, because then it is equilateral triangle.
        float height = (maxDis / 2) * sqrtOf3 + 10; // added ten for better sight on marbles
        // minimum height
        if (height < minHeight)
            height = minHeight;

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
