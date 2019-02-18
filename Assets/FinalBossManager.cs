using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalBossManager : MonoBehaviour {

    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] GameObject obj3;
    
    //items to destroy
    [SerializeField] GameObject obj4;
    [SerializeField] GameObject obj5;
    [SerializeField] GameObject obj6;

    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject rabbitTwo;

    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		


        if(rabbit.GetComponent<Health>().currentHealth <= -1)
        {
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(false);
            obj6.SetActive(true);
            rabbitTwo.SetActive(true);
        }

	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("F"))
        {
            Destroy(obj4);

            obj5.SetActive(false);
            obj1.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(true);

        }
    }
}
