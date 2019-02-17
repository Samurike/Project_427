using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject cyclops;
    Animator animO;
    Animator animP;


    [SerializeField] GameObject ah;
    [SerializeField] GameObject bah;

    [SerializeField] Transform a;
    [SerializeField] Transform b;

    // Use this for initialization
    void Start () {
        animO = cyclops.GetComponent<Animator>();
        animP = player.GetComponent<Animator>();

        bah.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        //Intro Cam
        if (ah.activeInHierarchy)
        {
            if (ah.transform.position.z <= 315) { animO.SetInteger("animation", 1); }
            if (ah.transform.position.z >= 270) { ah.transform.Translate(Vector3.forward * 60 * Time.deltaTime); }
            else
            {
                ah.SetActive(false);
                bah.SetActive(true);
            }
        }
	}
}
