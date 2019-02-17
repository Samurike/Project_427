using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    GameObject player;

    [SerializeField] float projectileDamage = 6f;

    [SerializeField] float manaCost = -5f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Health playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        rb = GetComponent<Rigidbody>();

        if(playerHP.currentEnergy > 5)
        {
            playerHP.getMana(manaCost);
        }
        
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().currentHealth =
              collision.gameObject.GetComponent<Health>().currentHealth - projectileDamage;
        }


        Destroy(gameObject);

        
    }
}
