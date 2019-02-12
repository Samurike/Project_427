using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingQuest : MonoBehaviour {

    public Text questText;
    int num;
    int len;
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        num = 0;
        enemies = (GameObject.FindGameObjectsWithTag("Enemy"));
        len = enemies.Length;
	}
	
	// Update is called once per frame
	void Update () {

        enemies = (GameObject.FindGameObjectsWithTag("Enemy"));

        if (num == len)
        {
            questText.text = "Return the Rabbit";
        }
        else
        {
            questText.text = "Kill Wolves Around the Village (" + num + " / " + len + ")";
        }


        if (Input.GetKeyDown(KeyCode.T)){
            num ++;
        }

	}

    public void Increment()
    {
        num++;
    }
}
