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
        center = new Vector3(transform.position.x, 0.2f, transform.position.z);
        x = Random.Range(-10.0f, 10.0f);
        z = Random.Range(-10.0f, 10.0f);
        newPos = new Vector3(center.x + x, -.2f, center.z + z);
        StartCoroutine("Rotate");
        Debug.Log(transform.position.y);
    }

	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case states.wander:

                if (System.Math.Round((newPos.x) ,1) == System.Math.Round((transform.position.x), 1))
                {
                    x = Random.Range(10.0f, 16.0f);
                    if(Random.value > .5f)
                    {
                        x *= -1;
                    }
                    z = Random.Range(10f, 16.0f);
                    if (Random.value > .5f)
                    {
                        z *= -1;
                    }
                    newPos = new Vector3(center.x + x, -.2f, center.z + z);
                    Debug.Log(newPos);
                    StartCoroutine("Rotate");
                }
                 Wander();
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
        Quaternion finish = Quaternion.LookRotation(newPos);
        float startTime = Time.time;
        float eTime = Time.time - startTime;
        while (eTime <= 1)
        {
            eTime = Time.time - startTime;
            transform.rotation = Quaternion.Lerp(start, finish, eTime);
            yield return new WaitForEndOfFrame();
        }
        transform.LookAt(newPos);

    }

    private void Wander()
    {
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
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
