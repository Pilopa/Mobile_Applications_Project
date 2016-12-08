using UnityEngine;

public class OneFinishScript : MonoBehaviour {

    private GameObject[] marbles;
    private Collider col;
    private HUD_Class ui;
    
    // Use this for initialization
    void Start () {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        col = GetComponent<Collider>();
        ui = (GameObject.FindGameObjectWithTag("UI")).GetComponent<HUD_Class>();
    }
	
	// Update is called once per frame
	void Update () {
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
