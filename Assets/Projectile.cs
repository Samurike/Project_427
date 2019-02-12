using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    GameObject player;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
