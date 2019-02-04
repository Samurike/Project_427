using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    private bool wait;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void takeDamage(float num)
    {
        if (!wait)
        {
            currentHealth -= num;
            Debug.Log(currentHealth);
            if(currentHealth <= 0)
            {
                Debug.Log("He a Dead Boi Now");
            }
        }

    }
}
