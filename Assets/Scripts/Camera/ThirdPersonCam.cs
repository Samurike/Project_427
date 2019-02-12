using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{

    public Transform targetPlayer, aimCast;
    private Transform t;
    Camera playerCam;
    public float cameraDistance = 5.0f; // Camera Distance for Third Person View.
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    bool toggled;
    public int range;

    //For Mouse Look
    public Vector2 mouseLook;
    //For Mouse Rotate Axis
    public Vector2 rotateVert;
    //For X Rotate of Camera
    public Quaternion camRotateX;
    //For XY Rotate of Camera
    public Quaternion camRotateXY;

    public Vector3 lookOffset; //For Third Person Look.


    // Use this for initialization
    void Start()
    {
        playerCam = GetComponent<Camera>();
        lookOffset = playerCam.transform.position - targetPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        CameraControl();

        if (Input.GetKeyDown(KeyCode.I))
        {
            toggled = false; ;
        }


        /*
        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit hit;
            if (Physics.Raycast(aimCast.transform.position, aimCast.transform.forward, out hit, range))
            {
                if (hit.transform.root.transform.tag == "Enemy")
                {
                    Debug.Log("derp");
                    if (toggled) { toggled = false; } else { toggled = true; }

                    t = hit.transform.GetChild(0).transform;
                    Debug.DrawLine(aimCast.transform.position, hit.transform.position, Color.cyan, 1f);
                }
            }
        }*/



    }

    public void CameraControl()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        rotateVert.x = Mathf.Lerp(rotateVert.x, md.x, 1f / smoothing);
        rotateVert.y = Mathf.Lerp(rotateVert.y, md.y, 1f / smoothing);
        mouseLook += rotateVert;
        //Setting Angle for the Y Rotation
        mouseLook.y = Mathf.Clamp(mouseLook.y, -40, 40);


        //Rotate on Both X and Y Use this Camera Method
        if (!toggled)
        {
            camRotateXY = Quaternion.Euler(-mouseLook.y, mouseLook.x, 0);
        }




        //Rotate Player With Camera on Move and Rotate Camera Around the Player in Idle State

        Vector3 lookPoint = targetPlayer.transform.position;

        if (!toggled) { playerCam.transform.LookAt(lookPoint + lookOffset); }
        else { playerCam.transform.LookAt(t.position); }



        //Third Person Look With Both X and Y Rotate
        Vector3 position = targetPlayer.position - (camRotateXY * Vector3.forward * cameraDistance + new Vector3(0, -lookOffset.y, 0));

        //Turin it on for Both Axis Rotation
        playerCam.transform.rotation = camRotateXY;
        playerCam.transform.position = position;


    }
}

