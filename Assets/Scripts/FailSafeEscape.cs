using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailSafeEscape : MonoBehaviour {

    Scene scene;

    // Use this for initialization
    void Start () {
		scene = SceneManager.GetActiveScene(); ;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetButtonDown("Esc"))
        {
            SceneManager.LoadScene(scene.name);
        }

	}
}
