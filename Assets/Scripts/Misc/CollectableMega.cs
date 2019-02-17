using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMega : MonoBehaviour {


    [SerializeField] float healthAmount = 1f;
    [SerializeField] float manaAmount = 1f;

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
            if(this.tag == "Mana")
            {
                other.GetComponent<Health>().getMana(manaAmount);
                Destroy(gameObject);
            }
            

            if (this.tag == "Health")
            {
                other.GetComponent<Health>().getHealth(healthAmount);
                Destroy(gameObject);
            }

        }

    }


}
