using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform PlayerTransform;
    private Vector3 cameraOffset;
    public Transform targetSpot;
    private Transform t;

    [Range(0.01f, 1.5f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationsSpeed = 2.0f;

    public float speedH = 1.0f;

    public float scrollSpeed;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    public float range;
    public bool toggled;

    // Use this for initialization

    void Start()
    {

        cameraOffset = transform.position - PlayerTransform.position;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        if (Input.GetMouseButtonDown(1))
        {
            if (toggled)
            {
                toggled = false;
            }
            else
            {
                toggled = true;
            }
        }


        if (toggled)
        {
            RaycastHit hit;
            if (Physics.Raycast(targetSpot.transform.position, targetSpot.transform.forward, out hit, range))
            {

                if(hit.transform.tag == "Enemy")
                {
                    t = hit.transform.GetChild(0).transform;
                    Debug.DrawLine(targetSpot.transform.position, hit.transform.position, Color.cyan, 1f);
                }

            }
        }
        else
        {
            t = PlayerTransform.transform;
        }
    }


    // LateUpdate is called after Update methods
    void LateUpdate()
    {

        if (RotateAroundPlayer)
        {
            Quaternion camH = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, this.transform.up);
            Quaternion camV = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, -1 * this.transform.right);

            cameraOffset = camV * camH * cameraOffset;
        }



        Vector3 newPos = PlayerTransform.position + cameraOffset;



        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
        {

                transform.LookAt(t.transform);
        }
    }
}


  

