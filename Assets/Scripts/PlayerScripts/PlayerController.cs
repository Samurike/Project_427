using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform cam;

    public float speed = 5.0f;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    public bool dodge;
    public bool wait;
    private int num, change;
    private bool grounded;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        num = 2;
    }


    void Update()
    {
        if(this.GetComponent<Health>().currentHealth > 0)
        {
       
            var CharacterRotation = cam.transform.rotation;
            CharacterRotation.x = 0;
            CharacterRotation.z = 0;

            transform.rotation = CharacterRotation;

        }



        /*float lh = Input.GetAxisRaw("Horizontal");

        var newRotation = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);

        if (lh != 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), speedDampTime * turnSmoothing * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        }


        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);
        */
    }




    private void FixedUpdate()
    {
        if(this.GetComponent<Health>().currentHealth > 0)
        {
            if (Input.GetKey(KeyCode.W) && !(anim.GetCurrentAnimatorStateInfo(0).IsName("dodgeLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("dodgeRight")))
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
            if ((!wait && this.GetComponent<ChaScript>().combat))
            {
                if (anim.GetInteger("animation") == 50 && !this.GetComponent<ChaScript>().attacking)
                {
                    this.GetComponent<Health>().Iframess();
                    transform.Translate(Vector3.left * 30 * Time.deltaTime);
                }
                if (anim.GetInteger("animation") == 60 && !this.GetComponent<ChaScript>().attacking)
                {
                    this.GetComponent<Health>().Iframess();
                    transform.Translate(Vector3.right * 30 * Time.deltaTime);
                }
            }
        }
    }
}


