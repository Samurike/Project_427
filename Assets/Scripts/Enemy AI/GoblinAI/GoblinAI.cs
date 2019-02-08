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
    private int num;
    private bool wait;
    private Animator playerAnim;
    private float distance;

    public enum states { idle, damaged, attack, death, strafe };
    states currentState = states.idle;

    // Use this for initialization
    void Start()
    {
        num = 3;
        anim = this.GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("strafeLeftSwordShield_RM")) { transform.Translate(Vector3.left * 8 * Time.deltaTime); }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("strafeRightSwordShield_RM")) { transform.Translate(Vector3.right * 8 * Time.deltaTime); }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walkForwardSwordShield")) { transform.Translate(Vector3.forward * 8 * Time.deltaTime); }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("runSwordShield_RM"))
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 18) { transform.Translate(Vector3.forward * 20 * Time.deltaTime); }
            else
            {
                Debug.Log("sda");
                anim.SetInteger("animation", 21);
            }

        }

        //float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (this.GetComponent<Health>().currentHealth == 0)
        {
            currentState = states.death;
        }
        switch (currentState)
        {
            case states.idle:
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 1);
                break;
            case states.attack:
                anim.SetInteger("moving", 2);
                Attacking();
                break;
            case states.strafe:
                anim.SetInteger("moving", 1);
                Strafe();
                break;
            case states.death:
                anim.SetInteger("moving", 0);
                anim.SetInteger("animation", 0);
                break;

        }

        if (distance < 60)
        {
            currentState = states.strafe;
            transform.LookAt(player.transform);
        }


    }

    private void Strafe()
    {
        
        if (!player.GetComponent<ChaScript>().attacking)
        {
            
            if(distance< 25)
            {
                float startTime = Time.time;
                float eTime = Time.time - startTime;


                if (eTime > 4) { currentState = states.attack; }


                anim.SetInteger("animation", num);
                switch (num)
                {
                    case 3:
                        anim.SetInteger("animation", 3);
                        if (!wait) {StartCoroutine("ChangeNum"); }                      
                        break;
                    case 4:
                        if (!wait)
                            StartCoroutine("ChangeNum");
                        break;
                    case 5:
                        if (!wait)
                            StartCoroutine("ChangeNum");
                        break;
                }
            }
            else
            {
                anim.SetInteger("animation", 7);
            }
        }
    }

    private void Attacking()
    {
        anim.SetInteger("animation", 21);
    }

    private IEnumerator ChangeNum()
    {
        wait = true;
        var x = Random.Range(4, 6);
        num = x;
        yield return new WaitForSeconds(1.2f);      
        Debug.Log(num);
        wait = false;


    }
}
