using UnityEngine;
using System.Collections;

public class Passage2 : MonoBehaviour
{


    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPos;
        v.x -= 2.0f * Mathf.Sin(Time.time * 3.0f);
        transform.position = v;
    }
}
