using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage;
    private bool wait = false;
    public GameObject player;
    private bool w;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !wait)
            StartCoroutine("Damage", other);

    }

    private IEnumerator Damage(Collider other)
    {
        other.GetComponent<Health>().takeDamage(damage);
        wait = true;
        yield return new WaitForSeconds(.5f);
        wait = false;
        
            
    }
}
