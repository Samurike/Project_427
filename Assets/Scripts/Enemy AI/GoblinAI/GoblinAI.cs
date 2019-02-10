using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{



    private Animator anim;
    [SerializeField] GameObject player;

    [SerializeField] private float speed = 5f;

    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackRadius = 5f;
    private int num, cNum, attackNum;
    private bool wait;
    private Animator playerAnim;
    private float distance;

    public enum states { idle, damaged, attack, death, defend, chase };
    states currentState = states.idle;

    private bool mode;

    // Use this for initialization
    void Start()
    {
        num = 20;
        attackNum = 21;
        anim = this.GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("strafeLeftSwordShield_RM")) { transform.Translate(Vector3.left * 8 * Time.deltaTime); }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("strafeRightSwordShield_RM")) { transform.Translate(Vector3.right * 8 * Time.deltaTime); }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walkForwardSwordShield_RM")) { transform.Translate(Vector3.forward * 18 * Time.deltaTime); }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("runSwordShield_RM")) { transform.Translate(Vector3.forward * 31 * Time.deltaTime); }


        //float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (this.GetComponent<Health>().currentHealth == 0)
        {
            currentState = states.death;
        }
        switch (currentState)
        {
            case states.idle:

                if (distance < 80 && distance > 20 && !(anim.GetCurrentAnimatorStateInfo(0).IsName("idleSlingshotToAimSlingshot") || anim.GetCurrentAnimatorStateInfo(0).IsName("shootSlingShot")))
                {
                    currentState = states.chase;

                }
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 1);
                break;
            case states.attack:
                mode = false;
                anim.SetInteger("moving", 2);
                Attack();
                break;
            case states.chase:
                mode = true;
                transform.LookAt(player.transform);
                anim.SetInteger("animation", 11);
                break;
            case states.defend:
                anim.SetInteger("moving", 1);
                Defend();
                break;
            case states.death:
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 0);
                break;

        }


        if (distance > 13 && distance <=18)
        {
            if (mode)
            {

                int rand = Random.Range(1, 3);
                Debug.Log(rand);
                mode = false;
                if (rand == 1)
                {
                    currentState = states.attack;
                }
                else { anim.SetInteger("animation", 3);  currentState = states.defend; }
            }

        }
        else if(distance < 10)
        {
            currentState = states.attack;
        }


    }

    private void Defend()
    {
        transform.LookAt(player.transform);
        mode = false;
        Debug.Log("def");
        float startTime = 0;
        if (!player.GetComponent<ChaScript>().attacking)
        {

            if (distance < 21)
            {


                anim.SetInteger("animation", num);
                switch (num)
                {
                    case 20:
                        startTime = Time.time;
                        if (!wait) { StartCoroutine("ChangeNum"); }
                        break;
                    case 18:
                        if (!wait)
                            StartCoroutine("ChangeNum");
                        break;
                    case 19:
                        if (!wait)
                            StartCoroutine("ChangeNum");
                        break;
                }
            }
            else
            {

                if (distance > 30 && !(anim.GetCurrentAnimatorStateInfo(0).IsName("idleSlingshotToAimSlingshot") || anim.GetCurrentAnimatorStateInfo(0).IsName("shootSlingShot")))
                { currentState = states.chase; } else { anim.SetInteger("animation", 6);}
            }
        }


        float eTime;
        eTime = Time.time - startTime;
        //Debug.Log(eTime);
        if(eTime> startTime+ 6) { currentState = states.attack; }
    }

    private void Attack()
    {
        transform.LookAt(player.transform);
        Debug.Log("tack");
        


        if(distance < 15)
        {
            anim.SetInteger("animation", attackNum);
            switch (attackNum)
            {            
                case 20:
                    if (!wait) { StartCoroutine("ChangeAttack"); }              
                    break;
                case 21:
                    if (!wait) { StartCoroutine("ChangeAttack"); }
                    break;
            }
        }

        if(distance> 18 && distance< 28){ anim.SetInteger("animation", 7); }


    }

    private IEnumerator ChangeNum()
    {
        wait = true;
        var x = Random.Range(18, 21);
        num = x;
        yield return new WaitForSeconds(1.2f);
        wait = false;
    }

    private IEnumerator ChangeAttack()
    {
        wait = true;
        var x = Random.Range(20, 22);
        attackNum = x;
        yield return new WaitForSeconds(1.2f);
        wait = false;
    }


    public void Fire()
    {Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("shootSlingshot"));
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idleSlingshotToAimSlingshot")) { anim.SetInteger("animation", 8);}
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("shootSlingShot")) {  anim.SetInteger("animation", 7); }

    }
}
