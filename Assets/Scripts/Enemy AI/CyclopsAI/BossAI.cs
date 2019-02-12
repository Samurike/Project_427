using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {


    public GameObject player;
    public GameObject placeHolder;
    Animator anim;
    public enum states { idle, grounded, damaged, attack, chase, death };
    states currentState = states.chase;

    private bool wait;


    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(this.GetComponent<Health>().currentHealth == 0)
        {
            currentState = states.death;
        }
        switch (currentState)
        {
            case states.idle:
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 1);
                break;
            case states.chase:
                anim.SetInteger("moving", 1);
                Chase();
                break;
            case states.attack:
                anim.SetInteger("moving", 2);
                Attack();
                break;
            case states.death:
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 0);
                break;

        }

	}


    private void Chase()
    {
        if(Vector3.Distance(player.transform.position, transform.position) > 15)
        {
            transform.LookAt(player.transform);
            anim.SetInteger("animation", 2);
            transform.Translate(Vector3.forward * 28 * Time.deltaTime);
        }
        else
        {
            anim.SetInteger("animation", 1);
            currentState = states.attack;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 20)
        {
            transform.LookAt(player.transform);
            if (!wait) { StartCoroutine("NextAttack"); }   
        }
    }

    private IEnumerator NextAttack()
    {
        wait = true;
        anim.SetInteger("animation", 1);
        yield return new WaitForSeconds(.8f);
        float num = Random.value;
        if(num > .5f)
        {
            anim.SetInteger("animation", 21);
        } else
        {
            anim.SetInteger("animation", 22);
        }
    }

    public void ComboCheck()
    {
        int num = Random.Range(1,5);
       // Debug.Log(num);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("leftHandAttackForward") && (num == 1 || num == 2 )){
            anim.SetInteger("animation", 22);
            //Debug.Log("now Right");
            wait = true;
        }else if (anim.GetCurrentAnimatorStateInfo(0).IsName("rightHandAttackForward") && (num == 1 || num == 2))
        {
            anim.SetInteger("animation", 21);
            //Debug.Log("now Left");
            wait = true;
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("rightHandAttackForward") || anim.GetCurrentAnimatorStateInfo(0).IsName("leftHandAttackForward")) && num == 4)
        {
            anim.SetInteger("animation", 33);
            //Debug.Log("Boffa");
            wait = true;
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("rightHandAttackForward") || anim.GetCurrentAnimatorStateInfo(0).IsName("leftHandAttackForward")) && num == 3)
        {
            anim.SetInteger("animation", 35);
           // Debug.Log("Rawr");
            wait = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("2HitComboAttackForward"))
        {
            anim.SetInteger("animation", 1);
            wait = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("roar"))
        {
            anim.SetInteger("animation", 1);
            wait = true;
        }
        else
        {
            wait = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Player")
        {
            Debug.Log(other.transform.name);
            currentState = states.chase;
        }

    }
}
