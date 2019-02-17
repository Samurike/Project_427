using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {


    [SerializeField] float healthAmount = 1f;
    [SerializeField] float manaAmount = 1f;
    [SerializeField] float radius = 10f;
    [SerializeField] float moveSpeed = 100f;
    float dist;

    FloatObject floatScript;

    Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("HPSpot");

        floatScript = GetComponent<FloatObject>();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(player);
        dist = Vector3.Distance(player.transform.position, this.transform.position);

        if (dist < radius)
        {
            floatScript.enabled = false;
            SeekTarget();
        }
        else
        {
            floatScript.enabled = true;
        }

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

    void SeekTarget()
    {
        //Subtracting two vectors by each will result in the desired direction
        Vector3 dir = player.position - transform.position;

        Vector3 moveVector = dir.normalized * moveSpeed * Time.deltaTime;

        transform.position += moveVector;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
