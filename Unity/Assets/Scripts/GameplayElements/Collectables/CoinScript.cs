using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    /// <summary>
    /// Amount of points added to the score, when the coin is collected
    /// </summary>
    public int Points = 500;
    private HUD_Class ui;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<HUD_Class>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Marble")
        {
            // adding points to score
            ui.Score += Points; 
            // Make object invisible ny disabling the renderer
            GetComponent<Renderer>().enabled = false;
            // enable this script to prevent the coin to be collected multiple times
            this.enabled = false;
        }
    }
}
