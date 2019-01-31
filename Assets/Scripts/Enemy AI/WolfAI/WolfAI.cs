using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour {

    Animator anim;
    private float x;
    private float z;
    Vector3 center;
    Vector3 newPos;
    public bool wait;
    public float radius = 1f;
    public enum states {wander , combat, chase, death };
    states currentState = states.wander;

    

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case states.wander:
                if (float.Parse(newPos.x.ToString("F1")) == float.Parse(transform.position.x.ToString("F1")))
                {
                    x = Random.Range(-20.0f, 20.0f);
                    z = Random.Range(-20.0f, 20.0f);
                    newPos = new Vector3(center.x + x, -.8f, center.z + z);
                }
                // Wander();
                Debug.Log(newPos);
                StartCoroutine("Rotate");
                break;
        }


        if (transform.GetComponent<Health>().currentHealth == 0)
        {
            anim.SetBool("dead", true);
        }
    }

    private IEnumerator Rotate()
    {
        Quaternion start = transform.rotation;
        Quaternion finish = Quaternion.FromToRotation(transform.forward,newPos - transform.position);
        float startTime = Time.time;
        float eTime = Time.time - startTime;
        while (eTime <= 1)
        {
            eTime = Time.time - startTime;
            transform.rotation = Quaternion.Lerp(start, finish, eTime);
            yield return new WaitForEndOfFrame();
        }

    }

    private void Wander()
    {
        //anim.SetInteger("animation", 1);

            transform.LookAt(newPos);
            transform.Translate(Vector3.forward * 8 * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            transform.LookAt(other.transform);

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            transform.LookAt(other.transform);
            anim.SetBool("run", true);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, radius);
    }

}
