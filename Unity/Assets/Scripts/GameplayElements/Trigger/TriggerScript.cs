using UnityEngine;
using Assets.Scripts.GameplayElements;

public class TriggerScript : MonoBehaviour {
    /// <summary>
    /// Reference to the object which should be triggered
    /// </summary>
    public GameObject TriggeredObject;
    /// <summary>
    /// Shows if marble is on the trigger
    /// </summary>
    private bool isActivated = false;
    private ITriggerable triggeredScript;

	// Use this for initialization
	void Start () {
        triggeredScript = TriggeredObject.GetComponent<ITriggerable>();
	}
	
	// Update is called once per frame
	void Update (){ }

    void OnTriggerEnter(Collider col)
    {
        isActivated = true;
        triggeredScript.TriggerEnter(col);
    }

    void OnTriggerExit(Collider col)
    {
        isActivated = true;
        triggeredScript.TriggerExit(col);
    }
}