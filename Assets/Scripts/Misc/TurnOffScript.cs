using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffScript : MonoBehaviour {

    bool act = false;

	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
		
        

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (act)
            {
                act = false;
            }
            else
            {
                act = true;
            }

            transform.Find("Menu").gameObject.SetActive(act);
        }

	}
}
