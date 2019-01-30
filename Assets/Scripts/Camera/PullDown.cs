using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDown : MonoBehaviour {


    private bool grounded;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <.016f)
            transform.position = new Vector3(transform.position.x, transform.position.y +.001f, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.transform.name);
        grounded = true;
    }
}
