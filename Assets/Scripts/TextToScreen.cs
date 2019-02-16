using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToScreen : MonoBehaviour {

    [SerializeField] Text txt;
    [SerializeField] Text txtOutline;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            txt.enabled = true;
            txtOutline.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            txt.enabled = false;
            txtOutline.enabled = false;
        }
    }
}
