using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour {

    Animator anim;
    

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


        if(transform.GetComponent<Health>().currentHealth == 0)
        {
            anim.SetBool("dead", true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            transform.LookAt(other.transform);

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            transform.LookAt(other.transform);
            anim.SetBool("run", true);
        }

    }

}
