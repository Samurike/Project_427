using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAreaDisplay : MonoBehaviour
{

    public GameObject Pole;
    public float minX, MaxX;
    public float minZ, MaxZ;


    private float x;
    private float z;
    private Vector3 center;
    private Vector3 newPos;


    private void Start()
    {
        center = new Vector3(transform.position.x, 0.2f, transform.position.z);
        //newPos = new Vector3(center.x + x, -.2f, center.z + z);
    }
    void Update()
    {

            x = Random.Range(minX, MaxX);
            if (Random.value > .5f)
            {
                x *= -1;
            }
            z = Random.Range(minZ, MaxZ);
            if (Random.value > .5f)
            {
                z *= -1;
            }

            newPos = new Vector3(center.x + x, -.2f, center.z + z);
            Instantiate(Pole, newPos, Quaternion.identity);

        }

    }
