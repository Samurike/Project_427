using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawner : MonoBehaviour {

    [SerializeField] GameObject prefabToUse;
    bool wait;

    [SerializeField] float minTime = 5f;
    [SerializeField] float maxTime = 10f;

    // Use this for initialization
    void Start () {
		
        

	}
	
	// Update is called once per frame
	void Update () {
        if(!wait)
        {
            StartCoroutine(spawn());
        }
        

	}

    IEnumerator spawn()
    {
        wait = true;
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Instantiate(prefabToUse, transform.position, Quaternion.identity);
        wait = false;
    }

}
