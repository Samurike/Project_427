using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : MonoBehaviour {

    [SerializeField] float carrotDmg;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Health>().takeDamage(carrotDmg); 
        }

        if (transform.gameObject.tag != "BarrierCarrot")
        {
            Debug.Log("Hi");
            Destroy(gameObject, 0.3f);
        }



    }
}
