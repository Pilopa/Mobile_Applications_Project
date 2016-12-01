using UnityEngine;
using System.Collections;

public class MultipleFinishesScript : MonoBehaviour {

    public bool IsTriggered{ get; private set; }

    void Start()
    {
        IsTriggered = false;
    }

    void OnTriggerEnter(Collider col)
    {
        IsTriggered = true;
    }

    void OnTriggerExit(Collider col)
    {
        IsTriggered = false;
    }
}
