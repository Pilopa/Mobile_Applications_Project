using UnityEngine;

public class OneFinishScript : MonoBehaviour {

    private GameObject[] marbles;
    private Collider col;
    private bool finished = false;
    private bool paused = false; //for temporary solution
    private HUD_Class ui;
    // Use this for initialization
    void Start () {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        col = GetComponent<Collider>();
        ui = (GameObject.FindGameObjectWithTag("UI")).GetComponent<HUD_Class>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (finished && !paused)
        //{
            
        //    return;
        //}

        bool allAtFinish = true;
        for (int i=0; i< marbles.Length; i++)
        {
            if (!col.bounds.Contains(marbles[i].transform.position))
                allAtFinish = false;   
        }
        if (allAtFinish)
        {
            ui.Finish();

        }
	}
}
