using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour {



    private Animator anim;
    private Rigidbody rb;
    private float speed = 5f;

    [SerializeField] Transform baseLocation;
    [SerializeField] Transform waypoint1;
    [SerializeField] Transform waypoint2;

    // Use this for initialization
    void Start() {

        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update() {

        StartCoroutine(Wander());
    }


    private IEnumerator Wander()
    {

        yield return new WaitForSeconds(4f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        yield return new WaitForSeconds(4f);

        transform.Translate(Vector3.back * speed * Time.deltaTime);

    } 

}
