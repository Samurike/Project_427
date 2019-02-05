using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    public Image hpBar;
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

            if(transform.tag == "Enemy")
            {
                hpBar.fillAmount = currentHealth / maxHealth;
            }
            Debug.Log(currentHealth);
            if(currentHealth <= 0)
            {
                Debug.Log("He a Dead Boi Now");
            }
        }

    }
}
