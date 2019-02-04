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


    private float x;
    private float z;
    private Vector3 center;
    private Vector3 newPos;
    private bool findNext;
    private Animator anim;




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

        if (Vector3.Distance(Player.transform.position, transform.position) < 70 && currentState == states.wander)
        {
            currentState = states.chase;
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
                if (Vector3.Distance(Player.transform.position, transform.position) > 20)
                {
                    anim.SetInteger("animation", 11);
                }
                else { anim.SetInteger("animation", 10); }
                break;
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


    public void CombatStance()
    {
        Debug.Log("here");
        anim.SetInteger("animation", 10);
    }

    private IEnumerator turnTimer()
    {
        yield return new WaitForSeconds(3f);
        findNext = true;
        yield return new WaitForSeconds(.01f);
        findNext = false;
    }


}

