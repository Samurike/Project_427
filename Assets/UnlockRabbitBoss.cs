using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRabbitBoss : MonoBehaviour {

    [SerializeField] GameObject Bra0;
    [SerializeField] GameObject Bra1;
    [SerializeField] GameObject Bra2;
    [SerializeField] GameObject Bra3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Bra0.GetComponent<BonFire>().lit && Bra1.GetComponent<BonFire>().lit && Bra2.GetComponent<BonFire>().lit && Bra3.GetComponent<BonFire>().lit)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

    }
}
