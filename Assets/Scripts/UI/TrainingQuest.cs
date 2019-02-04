using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingQuest : MonoBehaviour {

    public Text questText;
    int num;

	// Use this for initialization
	void Start () {
        num = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(num == 3)
        {
            questText.text = "Return the Rabbit";
        }
        else
        {
            questText.text = "Kill Wolves Around the Village (" + num + " / 3)";
        }
        if (Input.GetKeyDown(KeyCode.T)){
            num ++;
        }

	}
}
