using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour {



    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    [SerializeField] float chaseRange = 5f;

    [SerializeField] float attackRadius = 5f;
    [SerializeField] float moveRadius = 5f;

    [SerializeField] Transform baseLocation;
    [SerializeField] Transform waypoint1;
    [SerializeField] Transform waypoint2;

    [SerializeField] ChaScript player;

    // Use this for initialization
    void Start() {

        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ChaScript>();

    }

    // Update is called once per frame
    void Update() {

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

    }


    private IEnumerator Wander()
    {
        yield return new WaitForSeconds(2f);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
