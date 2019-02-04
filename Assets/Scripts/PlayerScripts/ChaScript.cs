using UnityEngine;
using System.Collections;

public class ChaScript : MonoBehaviour
{
    float dickhead;
    Rigidbody rb;
    public GameObject sword;
    public GameObject shield;
    private Animator anim;

    private bool patience;

    public bool combat;
    private bool toggleWait;
    public bool attacking;

    public int num;
    public bool canClick;
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
        Physics.gravity = new Vector3(0, -4800f * Time.deltaTime, 0);

        //Starts the Timer for the Switch between Combat and NonCombat Mode.
        if (Input.GetKeyDown(KeyCode.Tab) && !toggleWait)
        {
            StartCoroutine("Toggle");
        }


        //Sets an animation integer up so that we can determine whether or not the player is in motion(Since the 
        //combat uses the same three attacks, they are both used in the same call just in different ways)
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idleCombat"))
        {
            anim.SetInteger("moving", 0);
            attacking = false;
        }
        else { anim.SetInteger("moving", 1); }

        //combat is a bool that determines whether or not we are in the combat stance.
        if (!combat)
        {
            Forward();
        }
        else
        {
            Combat();
        }

    }


    //Timer For Combat mode.
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

    //Timer for the click count. Too many clicks resulted in the count breaking, this keeps it from breaking.
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        canClick = true;
    }



    //This handles all NON-Combat motions such as running / walking / and idle without the weapon. 
    void Forward()
    { 

        //resets the click count incase of a break.
        num = 0;
        //Removes the sword and shield from the player.
        shield.SetActive(false);
        sword.SetActive(false);

        //Handles NON-Combat forward and backwards animations.
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
                this.GetComponent<PlayerController>().speed = townSpeed - 4;
            }
        }
        else
        {
            anim.SetFloat("walkNormal", 0f);
        }

        //Handles NON-Combat running animations.
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


    //Handles all combat abilites
    void Combat()
    {
        //Turns the sword and shield visible to the player
        shield.SetActive(true);
        sword.SetActive(true);


        this.GetComponent<PlayerController>().speed = combatSpeed;
        //Call for direction movement for combat.
        Moving();
        //Call for Attack if pressed.
        if (Input.GetMouseButtonDown(0)) {
            Attacking(); }



    }
    //Handles the direction movement for combat.
    void Moving()
    {

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkBackwardsCombat", true);
        }
        else
        {
            anim.SetBool("walkBackwardsCombat", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("walkForwardsCombat", true);
            if (Input.GetKey(KeyCode.LeftShift)) { Running(); }
            else
            {
                 anim.SetBool("runNormal", false);
                 this.GetComponent<PlayerController>().speed = combatSpeed;
            }

        }
        else
        {
            anim.SetBool("walkForwardsCombat", false);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkLeftCombat", true);
            if (Input.GetKeyDown(KeyCode.Space) && !this.GetComponent<PlayerController>().wait)
            {
                anim.SetBool("walkLeftCombat", false);
                anim.SetInteger("animation", 50);
            }
        }
        else
        {
            anim.SetBool("walkLeftCombat", false);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("walkRightCombat", true);
            if (Input.GetKeyDown(KeyCode.Space) && !this.GetComponent<PlayerController>().wait)
            {
                anim.SetBool("walkRightCombat", false);
                anim.SetInteger("animation", 60);
            }
        }
        else
        {
            anim.SetBool("walkRightCombat", false);
        }
    }

    //Handles Running and running attack.
    void Running()
    {
        anim.SetBool("runNormal", true);
        this.GetComponent<PlayerController>().speed = runSpeed;

        //Slows down the player if they are attacking while running.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            this.GetComponent<PlayerController>().speed = combatSpeed;
        }

    }



    //Handles all Measures of attacks.
    void Attacking()
    {
        attacking = true;
        //keeps a click counter for combo
        if (canClick && num<=3)
        {
            num++;
            sword.GetComponent<Weapon>().Increase();
            
        }
        //determines what attack will be done.. granted we have only one combo, we have one set of animations that
        //play when the player is moving vs when the player is standing still. The third id
        if(num == 1){

            if (anim.GetInteger("moving") == 0)
            {
                anim.SetInteger("animation", 10);
            }
            if (anim.GetInteger("moving") == 1)
            {
                if (anim.GetBool("runNormal"))
                {
                    anim.SetInteger("animation", 110);
                }
                else
                {
                    anim.SetInteger("animation", 10);
                }

            }
            damage = true;
        }
    }
    //checks to see what conditions have been met to proceed to attack/dodge in specific ways. the int values for
    //the animations are assigned freely, 1 is idle, 10 is attack3, 20 is attack2... so on and so on.
    //they can be assigned at your leisure.
    public void ComboCheck()
    {
        attacking = true;
        canClick = false;


        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("attack3") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack3f")) && num == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            anim.SetInteger("animation", 1);
            StartCoroutine("Wait");
            num = 0;

        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("attack3") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack3f")) && num >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo 
            damage = true;
            anim.SetInteger("animation", 20);
            StartCoroutine("Wait");
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2f")) && num == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle    
            anim.SetInteger("animation", 1);
            StartCoroutine("Wait");
            num = 0;
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2f")) && num >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo
            damage = true;
            anim.SetInteger("animation", 30);
            StartCoroutine("Wait");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack4") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack4f"))
        { //Since this is the third and last animation, return to idle          
            anim.SetInteger("animation", 1);
            StartCoroutine("Wait");
            num = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("attackRunning1"))
        { //Attacks horizontally if left clicked while running.         
            anim.SetInteger("animation", 1);
            this.GetComponent<PlayerController>().speed = combatSpeed;
            StartCoroutine("Wait");
            num = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("dodgeLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("dodgeRight"))
        { //Attacks horizontally if left clicked while running.         
            anim.SetInteger("animation", 1);
            this.GetComponent<PlayerController>().speed = combatSpeed;
            StartCoroutine("Wait");
            num = 0;
        }


    }


}
