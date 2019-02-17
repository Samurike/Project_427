using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour {

    [SerializeField] float radius = 10f;
    [SerializeField] float moveSpeed = 100f;
    float dist;

    Transform player;

    Vector3 rP;
    private Quaternion to;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform.Find("HPSpot");
        
    }
	
	// Update is called once per frame
	void Update () {

        

        //transform.LookAt(player);
        rP = player.transform.position - transform.position;
        to = Quaternion.LookRotation(rP);

        transform.rotation = to;

        dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist < radius)
        {
            SeekTarget();
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
