using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float damage;
    public GameObject player;
    private Animator anim;
    private bool check;

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Collider>().enabled = false;

        check = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.transform.tag == "Enemy")
        {
            check = true;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack3") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack3f"))
            {                
                other.transform.root.GetComponent<Health>().takeDamage(damage); 
                
            }else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2f"))
            {
                other.transform.root.GetComponent<Health>().takeDamage(damage*2);
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack4") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack4f"))
            {
                other.transform.root.GetComponent<Health>().takeDamage(damage*2);
            }

        }
    }

    public void Increase()
    {
        StartCoroutine("Toggle");
    }

    private IEnumerator Toggle()
    {
        this.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(.01f);
        this.GetComponent<Collider>().enabled = false;
    }
}
