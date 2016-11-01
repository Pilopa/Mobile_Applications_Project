using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour {
    private GameObject[] marbles;
    private Collider col;
    private bool finished = false;
    // Use this for initialization
    void Start () {
        marbles = GameObject.FindGameObjectsWithTag("Marble");
        col = GetComponent<Collider>();
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
            finished = true;
	}

    void OnGUI()
    {
        if (finished)
            GUI.TextField(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Finish!");
    }
}
