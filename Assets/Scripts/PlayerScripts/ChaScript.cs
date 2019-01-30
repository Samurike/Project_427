using UnityEngine;
using System.Collections;

public class ChaScript : MonoBehaviour
{

    Rigidbody rb;
    public GameObject sword;
    public GameObject shield;
    private Animator anim;

    private bool patience;

    public bool combat;
    private bool toggleWait;
    public bool attacking;

    private int animNum;
    private int num;
    private bool canClick;
    private int moving;
    public bool damage;


    Collider m_Collider;
    public float runSpeed;
    public float combatSpeed;
    public float townSpeed;

    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        shield.SetActive(false);
        sword.SetActive(false);
        canClick = true;
        num = 0;
        m_Collider = sword.GetComponent<Collider>();


    }


    private void Update()
    {
        Physics.gravity = new Vector3(0, -4800f* Time.deltaTime, 0);
        if (Input.GetKeyDown(KeyCode.Tab) && !toggleWait)
        {
            StartCoroutine("Toggle");
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idleCombat")) { 
            anim.SetInteger("moving", 0);
        }
        else { anim.SetInteger("moving", 1); ; }

    }
    void FixedUpdate()
    {



        if (!combat)
        {
            Forward();
        }
        else
        {
            Combat();
        }

    }

    private IEnumerator Toggle()
    {
        toggleWait = true;
        yield return new WaitForSeconds(.3f);
        if (combat)
        {
            anim.SetBool("combatMode", false);
            combat = false;
        }
        else
        {
            anim.SetBool("combatMode", true);
            combat = true;
        }
        toggleWait = false;
    }


    private IEnumerator Wait()
    {
        patience = true;
        yield return new WaitForSeconds(1f);
        patience = false;

    }



    //Handles Forward Animations
    void Forward()
    {
        shield.SetActive(false);
        sword.SetActive(false);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("walkNormal", 1f);

            if (Input.GetKey(KeyCode.W))
            {
                anim.speed = 1;
                anim.SetFloat("Direction", 1);
                this.GetComponent<PlayerController>().speed = townSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetFloat("Direction", -1);
                this.GetComponent<PlayerController>().speed = townSpeed-4;
            }
        }
        else
        {
            anim.SetFloat("walkNormal", 0f);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("runNormal", true);
            this.GetComponent<PlayerController>().speed = runSpeed;
        }
        else
        {
            anim.SetBool("runNormal", false);
            this.GetComponent<PlayerController>().speed = townSpeed;
        }

    }

    void Combat()
    {
        shield.SetActive(true);
        sword.SetActive(true);
        ///////////////////////////////////////////////////////////////////////////////////////////////
        this.GetComponent<PlayerController>().speed = combatSpeed;
        Moving();
        if (Input.GetMouseButtonDown(0)) { Attacking(); }

        Dodging();

    }

    void Moving()
    {
        anim.SetInteger("runAttack", 0);
        if (!patience) {Running();}


        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkBackwardsCombat", true);
        }
        else
        {
            anim.SetBool("walkBackwardsCombat", false);
        }

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkForwardsCombat", true);
        }
        else
        {
            anim.SetBool("walkForwardsCombat", false);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkLeftCombat", true);
        }
        else
        {
            anim.SetBool("walkLeftCombat", false);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkRightCombat", true);
        }
        else
        {
            anim.SetBool("walkRightCombat", false);
        }
    }

    void Dodging()
    { 
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("dodgeLeft", true);
        }
        else
        {
            anim.SetBool("dodgeLeft", false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("dodgeRight", true);
        }
        else
        {
            anim.SetBool("dodgeRight", false);
        }
    }

    void Running()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("runNormal", true);
            this.GetComponent<PlayerController>().speed = runSpeed;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                anim.SetInteger("runAttack", 1);
                anim.SetBool("runNormal", false);
                StartCoroutine("Wait");
                this.GetComponent<PlayerController>().speed = combatSpeed;
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                anim.SetInteger("runAttack", 2);
                anim.SetBool("runNormal", false);
                StartCoroutine("Wait");
                this.GetComponent<PlayerController>().speed = combatSpeed;
            }

        }
        else
        {
            anim.SetBool("runNormal", false);
            this.GetComponent<PlayerController>().speed = combatSpeed;
        }


    }




    void Attacking()
    {

        if (canClick && !Input.GetKey(KeyCode.LeftShift))
        {
            num++;
        }
        if(num == 1){

            if (anim.GetInteger("moving") == 0)
            {
                anim.SetInteger("animation", 10);
            }
            if (anim.GetInteger("moving") == 1)
            {
                anim.SetInteger("animation", 10);
            }
            damage = true;


        }
    }

    public void ComboCheck()
    {

        canClick = false;


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack3") && num == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            Debug.Log(0);
            anim.SetInteger("animation", 1);
            canClick = true;
            num = 0;

        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack3") && num >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo 
            damage = true;
            anim.SetInteger("animation", 20);
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") && num == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle    
            anim.SetInteger("animation", 1);

            canClick = true;
            num = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") && num >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo
            damage = true;
            anim.SetInteger("animation", 30);
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack4"))
        { //Since this is the third and last animation, return to idle          
            anim.SetInteger("animation", 1);
            canClick = true;
            num = 0;
        }




    }
}
