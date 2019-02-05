using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    public float maxEnergy;
    public float currentEnergy;

    [SerializeField] Image imgHP;
    [SerializeField] Image imgEN;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.tag == "Player")
        {
            imgHP.fillAmount = currentHealth / maxHealth;
            imgEN.fillAmount = currentEnergy / maxEnergy;
        }

    }

    public void takeDamage(float num)
    {

        currentHealth -= num;

        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("He a Dead Boi Now");
        }
    }
}
