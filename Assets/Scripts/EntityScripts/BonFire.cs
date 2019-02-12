using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonFire : MonoBehaviour {

    GameObject player;
    [SerializeField] float giveAmount;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () { 
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            player.GetComponent<Health>().giveHealth(giveAmount);
        }
    }
}
