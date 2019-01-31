using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform cam;

    public float speed = 5.0f;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    public float force = -20;
    public float thrust;
    private bool isGrounded;
    public bool dodge;
    public bool wait;
    private int num;
    private bool grounded;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }


    void LateUpdate()
    {

            float lh = Input.GetAxisRaw("Horizontal");


            var newRotation = new Vector3(cam.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);


            if (lh != 0f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), speedDampTime * turnSmoothing * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
               // rb.MoveRotation(transform.rotation);
            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
    



    private void FixedUpdate()
    {

        //transform.position = new Vector3(transform.position.x, -.081f, transform.position.z);

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && !(this.GetComponent<Animator>().GetBool("runNormal")) && this.GetComponent<ChaScript>().combat)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && !(this.GetComponent<Animator>().GetBool("runNormal")) && this.GetComponent<ChaScript>().combat)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }


        ///////////////////////////////////////////////////////
        ///
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) && !wait && this.GetComponent<ChaScript>().combat)
        {
            if (anim.GetInteger("animation") == 200 && !anim.GetCurrentAnimatorStateInfo(0).IsName("runNormal"))
            {
                num = 0;
                StartCoroutine("Dodge");
            }
            if (anim.GetInteger("animation") == 210 && !anim.GetCurrentAnimatorStateInfo(0).IsName("runNormal"))
            {
                num = 1;
                StartCoroutine("Dodge");
            }
        }


        if (dodge && num == 0)
            {
                transform.Translate(Vector3.left * 35 * Time.deltaTime);
            }
        if (dodge && num == 1)
        {
            transform.Translate(Vector3.right * 35 * Time.deltaTime);
        }

    }
        /////////////////////////////////////////////////////////
    
    private IEnumerator Dodge()
    {
        dodge = true;
        wait = true;
        yield return new WaitForSeconds(.5f);
        dodge = false;
        yield return new WaitForSeconds(2f);
        wait = false;
    }

}


