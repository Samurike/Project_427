using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{

    public enum states { wander, damaged, attack, chase, death };
    states currentState = states.chase;


    private Quaternion to;

    //public Quaternion to = Quaternion.LookRotation()
    public float Rotspeed = 1f;
    public float wanderSpeed, chaseSpeed;
    public GameObject Pole, Player;
    public float minX, MaxX;
    public float minZ, MaxZ;
    [SerializeField] float horseDmg = 1;

    [SerializeField] float health;
    [SerializeField] GameObject healthOrb;

    private float x;
    private float z;
    private Vector3 center;
    private Vector3 newPos;
    private bool findNext;
    private Animator anim;
    private int num;
    private bool wait;
    private bool dead;




    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = states.wander;

        x = Random.Range(10.0f, 20.0f);
        if (Random.value > .5f)
        {
            x *= -1;
        }
        z = Random.Range(10f, 20.0f);
        if (Random.value > .5f)
        {
            z *= -1;
        }

        center = new Vector3(transform.position.x, 0.2f, transform.position.z);
        newPos = new Vector3(center.x + x, -.2f, center.z + z);
        //Instantiate(Pole, newPos, Quaternion.identity);

        Vector3 relativePosition = newPos - transform.position;
        to = Quaternion.LookRotation(relativePosition);
        StartCoroutine("turnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponent<Health>().currentHealth;
        if (health <= 0)
        {
            for(int i = 0; i < 5; i++)
            {
                Instantiate(healthOrb, new Vector3(transform.position.x, transform.position.y + 6f, transform.position.z), Quaternion.identity);
            }
            
        }

        if(this.GetComponent<Health>().currentHealth <= 0)
        {
            if (!dead) { Camera.main.GetComponent<TrainingQuest>().Increment(); dead = true; }
            transform.Find("EnemyCanvas").gameObject.SetActive(false);
            currentState = states.death;
        }

        if (Vector3.Distance(Player.transform.position, transform.position) < 70 && currentState == states.wander)
        {
            currentState = states.chase;
            num = 0;
        }

        if (Vector3.Distance(Player.transform.position, transform.position) < 100 && !(Vector3.Distance(Player.transform.position, transform.position) < 70))
        {
            currentState = states.wander;
            if (num == 0) {StartCoroutine("turnTimer"); num++; }
            

        }


        switch (currentState)
        {
            case states.wander:
                anim.SetInteger("animation", 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, to, Rotspeed);
                transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);
                if (findNext)
                {
                    nextPos();
                }
                break;

            case states.chase:
                anim.SetInteger("animation", 2);
                Vector3 relativePosition = Player.transform.position - transform.position;
                to = Quaternion.LookRotation(relativePosition);
                transform.rotation = Quaternion.Lerp(transform.rotation, to, .03f);
                transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(Player.transform.position, transform.position) < 15){
                    anim.SetInteger("animation", 11);
                    currentState = states.attack;
                }
                break;

            case states.attack:
                transform.LookAt(Player.transform);
                if (Vector3.Distance(Player.transform.position, transform.position) > 22)
                {
                    anim.SetInteger("animation", 13);
                }
                if (!wait) { StartCoroutine("NextAttack"); }

                break;

            case states.death:
                anim.SetInteger("animation", 0);
                this.GetComponent<WolfAI>().enabled = false;
                break;
        }
    }


    private IEnumerator NextAttack()
    {
        wait = true;
        anim.SetInteger("animation", 10);
        yield return new WaitForSeconds(2.4f);
        float num = Random.value;
        anim.SetInteger("animation", 11);
    }

    public void ComboCheck()
    {
        //DamageCheck();
        int num = Random.Range(1, 3);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Bite"))
        {
            //Debug.Log("lolol");
            DamageCheck();
            anim.SetInteger("animation", 10);
            wait = false;
        }

        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("jumpBite_RM"))
        {
            DamageCheck();
            anim.SetInteger("animation", 12);
            //Debug.Log("now Left");
            wait = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("jumpBite_RM"))
        {
            DamageCheck();
            anim.SetInteger("animation", 10);
            //Debug.Log("Boffa");
            wait = false; 
        }
        else
        {
            wait = false;
        }

    }

    public void DamageCheck()
    {

        if (Vector3.Distance(Player.transform.position, transform.position) < 15)
        {
            Player.GetComponent<Health>().takeDamage(horseDmg);
        }
    }

   void nextPos()
    {
        x = Random.Range(minX, MaxX);
        if (Random.value > .5f)
        {
            x *= -1;
        }
        z = Random.Range(minZ, MaxZ);
        if (Random.value > .5f)
        {
            z *= -1;
        }

        newPos = new Vector3(center.x + x, -.2f, center.z + z);
        //Instantiate(Pole, newPos, Quaternion.identity);

        Vector3 relativePosition = newPos - transform.position;
        to = Quaternion.LookRotation(relativePosition);
        StartCoroutine("turnTimer");
    }


    //Finds the next position for the w
    private IEnumerator turnTimer()
    {
        yield return new WaitForSeconds(3f);
        findNext = true;
        yield return new WaitForSeconds(.01f);
        findNext = false;
    }

}

