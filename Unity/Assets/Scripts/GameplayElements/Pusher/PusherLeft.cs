using UnityEngine;
using System.Collections;

public class PusherLeft : MonoBehaviour {

    public bool quickcheck = true;

    private Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {


        //transform.position = new Vector3(Mathf.PingPong(Time.time + 19, 22), transform.position.y, transform.position.z);

        Vector3 v = startPos;
        v.x += 2.0f * Mathf.Sin(Time.time * 4.0f);
        transform.position = v;


        /*
        if (quickcheck == true)
        {
            //transform.Translate(1.0f, 0.0f, 0.0f, 10 * Time.deltaTime);
            //transform.Translate(Vector3.forward * Time.deltaTime*5);
            //transform.Translate(Vector3.right * speed * Time.deltaTime * 20);
            //Vector3 direction = Vector3.Normalize(startPosition - transform.position);
            //transform.position = transform.position + direction * Speed * Time.deltaTime;
            //transform.position = new Vector3(Mathf.PingPong(Time.time, 5), transform.position.y, transform.position.z);
            transform.Translate(Time.deltaTime, 0, 0);
            quickcheck = false;
        }
        else if (quickcheck == false)
        {
            //transform.Translate(-1.0f, 0.0f, 0.0f, 10 * Time.deltaTime);
            //transform.Translate(Vector3.back * Time.deltaTime*5);
            //transform.Translate(-Vector3.right * speed * Time.deltaTime * 20);
            //Vector3 direction = Vector3.Normalize(startPosition - transform.position);
            //transform.position = transform.position + direction * Speed * Time.deltaTime;
            //transform.position = new Vector3(Mathf.PingPong(Time.time, 5), transform.position.y, transform.position.z);
            transform.Translate(Time.deltaTime, 0, 0);
            quickcheck = true;
        }
        */



    }
}
