using UnityEngine;
using System.Collections.Generic;

// FinishManager is only needed when multiple finishes exist in the scene
public class FinishManager : MonoBehaviour {

    public List<MultipleFinishesScript> Finishes;

    private bool allInFinish = false;
    private HUD_Class ui;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<HUD_Class>();
    }

	// Update is called once per frame
	void Update () {
        allInFinish = true;
        foreach (MultipleFinishesScript f in Finishes)
        {
            if (!f.IsTriggered)
                allInFinish = false;
        }
        if(allInFinish)
        {
            ui.Finish();
        }
	}
}
