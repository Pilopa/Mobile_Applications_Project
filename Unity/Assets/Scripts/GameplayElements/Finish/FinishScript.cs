using UnityEngine;

public class FinishScript : MonoBehaviour {
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
        if (finished && !paused)
        {
            ui.PauseAndResumeGame();
            paused = true;
            return;
        }

        bool allAtFinish = true;
        for (int i=0; i< marbles.Length; i++)
        {
            if (!col.bounds.Contains(marbles[i].transform.position))
                allAtFinish = false;   
        }
        if (allAtFinish)
            finished = true;
	}

    void OnGUI()
    {
        if (finished)
            GUI.TextField(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Finish!");
    }
}
