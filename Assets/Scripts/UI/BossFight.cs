using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour {

    [SerializeField] Text questText;
    [SerializeField] GameObject rabbit;
    int num = 0;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (rabbit.activeInHierarchy && num == 0)
        {
            questText.text = "Talk To Trix.";
            if (Input.GetKeyDown(KeyCode.F)) { num++; }
        }else if (rabbit.activeInHierarchy)
        {
            questText.text = "Go to The Cyclops Den.";
        }
        else
        {
            questText.text = "Defeat Twix.";
        }

    }
}