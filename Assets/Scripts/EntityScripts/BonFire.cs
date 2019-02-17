using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonFire : MonoBehaviour {

    GameObject player;
    [SerializeField] float fillBar;
    [SerializeField] float giveAmount;
    [SerializeField] GameObject Goblin0;
    [SerializeField] GameObject Goblin1;
    [SerializeField] GameObject Fire;
    [SerializeField] Image Bar;
    private float interactBar;
    float x;
    float maxAmount = 100;

    bool col;
    public bool lit;
    bool dead;
    [SerializeField] Canvas can;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        x = player.GetComponent<Health>().maxHealth;
        dead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!col){ can.gameObject.SetActive(false);}
        Bar.fillAmount = interactBar / maxAmount;

        if(Bar.fillAmount == 1)
        {
            try { if (!lit) { Camera.main.GetComponent<BraQuest>().Increment(); can.gameObject.SetActive(false); lit = true; }}
            catch { lit = true; }
            
            can.gameObject.SetActive(false);
            Fire.SetActive(true);
           
        }

        if(transform.name != "townBra")
        {
            if(Goblin0.GetComponent<Health>().currentHealth<= 0  && Goblin1.GetComponent<Health>().currentHealth <= 0)
            {
                dead = true;
            }
        }
        else { dead = true; }


    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            col = true;
            can.gameObject.SetActive(true);
        }

        if (Input.GetKey(KeyCode.C))
        {
            interactBar += 1f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        col = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(lit);
        if (other.transform.tag == "Player" && lit && dead)
            {
                player.GetComponent<Health>().giveHealth(giveAmount);
            }

    }
}
