using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingQuest : MonoBehaviour {

    public Text questText;
    int num;
    int len;
    int questNum = 0;
    public GameObject[] enemies;
    [SerializeField] Transform newPos;
    [SerializeField] Transform rabbit;

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
            rabbit.transform.position = newPos.transform.position;
            rabbit.transform.rotation = newPos.transform.rotation;
            questText.text = "Return the Rabbit At The Well";
            rabbit.Find("Tutorial Rabbit").GetComponent<CameraZoomInScreen>().QuestNum();
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