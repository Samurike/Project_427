using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraQuest : MonoBehaviour {

    public Text questText;
    int num;
    int len;
    public GameObject[] bras;

    // Use this for initialization
    void Start()
    {
        num = 0;
        bras = (GameObject.FindGameObjectsWithTag("Bra"));
        len = bras.Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (num == len)
        {
            questText.text = "Enter The Den Of the Monster";
        }
        else
        {
            questText.text = "Light the Braziers Around the Forest(" + num + " / " + len + ")";
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            num++;
        }

    }

    public void Increment()
    {
        num++;
    }
}
