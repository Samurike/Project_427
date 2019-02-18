using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDeathScene : MonoBehaviour {

    [SerializeField] string scene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(GetComponent<Health>().currentHealth <= 0)
        {
            SceneManager.LoadScene(scene);
            
        }

	}
}
