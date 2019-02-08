using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject Player;
    public Transform camSpot;
    private Vector3 cameraOffset;
    public Transform targetSpot;
    private Transform t;

    [Range(0.01f, 1.5f)]
    public float SmoothFactor = 0.5f;
    public float RotationsSpeed = 2.0f;

    public float speedH = 1.0f;

    public float scrollSpeed;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    public float range;
    public bool toggled;
    public float camDis;

    Vector3 newPos;

    // Use this for initialization

    void Start()
    {
        toggled = false;
        cameraOffset = transform.position - camSpot.position;
        camDis = Vector3.Distance(transform.position, camSpot.position);
        transform.LookAt(camSpot);

    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");



        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(targetSpot.transform.position, targetSpot.transform.forward, out hit, range))
            {
                if (hit.transform.root.transform.tag == "Enemy")
                {
                    if (toggled) { StartCoroutine("Delay"); } else { toggled = true; }
                    Debug.Log(newPos);
                    t = hit.transform.GetChild(0).transform;
                    Debug.DrawLine(targetSpot.transform.position, hit.transform.position, Color.cyan, 1f);
                }
            }
        } 
    }


    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(.03f);
        newPos = transform.position;
        Debug.Log(newPos);
        yield return new WaitForSeconds(.01f);
        toggled = false;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {

        if (!toggled)
        {
            Vector3 newPos;
            Quaternion camH = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, this.transform.up);
            Quaternion camV = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, -1 * this.transform.right);
            cameraOffset = camV * camH * cameraOffset;
            newPos = camSpot.position + cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
            transform.LookAt(camSpot);
        }
        else
        {
            transform.position = camSpot.position + camDis * (camSpot.position - t.position).normalized;
            transform.LookAt(t);
        }

    }
}




