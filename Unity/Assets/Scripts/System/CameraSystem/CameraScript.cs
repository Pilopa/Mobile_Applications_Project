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
        Vector3 sum = marbles[0].transform.position;
        for (int i = 1; i < marbles.Length; i++)
        {
            sum += marbles[i].transform.position;
        }
        sum = sum / marbles.Length;
        transform.position = new Vector3(sum.x, 0, sum.z); // y needs to be 0 in order to calculate the height later on

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
