using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomInScreen : MonoBehaviour {

    [SerializeField] float zoomIn;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("F"))
        {
            Camera.main.GetComponent<ThirdPersonCam>().cameraDistance = -1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Camera.main.GetComponent<ThirdPersonCam>().cameraDistance = 15;
    }
}
