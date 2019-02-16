using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

    [SerializeField] GameObject Bra0;
    [SerializeField] GameObject Bra1;
    [SerializeField] GameObject Bra2;
    [SerializeField] GameObject Bra3;
    [SerializeField] GameObject LDoor;
    [SerializeField] GameObject RDoor;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Bra0.GetComponent<BonFire>().lit && Bra1.GetComponent<BonFire>().lit && Bra2.GetComponent<BonFire>().lit && Bra3.GetComponent<BonFire>().lit)
        {
            if(LDoor.transform.position.z > 430)
            {
                LDoor.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            }

            if (RDoor.transform.position.z < 470)
            {
                RDoor.transform.Translate(Vector3.back * 3 * Time.deltaTime);
            }
        }
	}
}
