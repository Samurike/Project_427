using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapTransition : MonoBehaviour {

    [SerializeField] string sceneToGoTo;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("F"))
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
