using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCinematic : MonoBehaviour
{

    [SerializeField] GameObject aCam;
    [SerializeField] GameObject bCam;
    [SerializeField] GameObject cCam;
    [SerializeField] GameObject dCam;

    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] Transform c;
    [SerializeField] Transform d;


    // Use this for initialization
    void Start()
    {
        bCam.gameObject.SetActive(false);
        cCam.gameObject.SetActive(false);
        dCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //well!!!
        if (aCam.activeInHierarchy)
        {
            if (aCam.transform.position.z <= 254) { aCam.transform.Translate(Vector3.forward * 20 * Time.deltaTime); }
            else
            {
                if (aCam.transform.position.y >= 3.5) { aCam.transform.Translate(Vector3.down * 8 * Time.deltaTime); }
                else
                {
                    bCam.gameObject.SetActive(true);
                    aCam.gameObject.SetActive(false);
                    cCam.gameObject.SetActive(false);
                    dCam.gameObject.SetActive(false);
                }
            }

        }
        else { aCam.transform.position = a.position; }


        //BELL!!!!
        if (bCam.activeInHierarchy)
        {
            if (bCam.transform.position.x >= 343) { bCam.transform.Translate(Vector3.forward * 18 * Time.deltaTime); }
            else
            {
                cCam.gameObject.SetActive(true);   
                bCam.gameObject.SetActive(false);
                aCam.gameObject.SetActive(false);
                dCam.gameObject.SetActive(false);

            }
        }
        else { bCam.transform.position = b.position; }

        //FORESt
        if (cCam.activeInHierarchy)
        {
            if (cCam.transform.position.z >= 1664) { cCam.transform.Translate(Vector3.forward * 16 * Time.deltaTime); }
            else
            {
                dCam.gameObject.SetActive(true);
                bCam.gameObject.SetActive(false);
                aCam.gameObject.SetActive(false);
                cCam.gameObject.SetActive(false);
            }
        }
        else { cCam.transform.position = c.position; }

        if (dCam.activeInHierarchy)
        {
            if (dCam.transform.position.x <= 393) { dCam.transform.Translate(Vector3.forward * 25 * Time.deltaTime); }
            else
            {
                aCam.gameObject.SetActive(true);
                bCam.gameObject.SetActive(false);
                cCam.gameObject.SetActive(false);
                dCam.gameObject.SetActive(false);
            }
        }
        else { dCam.transform.position = d.position; }
    }

    }
