using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomInScreen : MonoBehaviour
{

    [SerializeField] float zoomIn;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject rabbitCam;
    Animator anim;
    private float distance;
    int i = 0;
    int qNum = 0;

    public enum states { talking, notTalking };
    states currentState = states.notTalking;

    [SerializeField] Image bg;
    TextAsset currentFile;
    [SerializeField] TextAsset textFile1;
    [SerializeField] TextAsset textFile2;
    [SerializeField] TextAsset textFile3;
    [SerializeField] Text text;
    string[] dialogLines;

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
        rabbitCam.SetActive(false);
        bg.gameObject.SetActive(false);
        currentFile = textFile1;
        if (currentFile != null)
        {
            dialogLines = (currentFile.text.Split('\n'));
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, rabbit.transform.position);

        if (distance <= 20)
        {
            switch (currentState)
            {
                case states.notTalking:
                    bg.gameObject.SetActive(false);
                    if (Input.GetButtonDown("F"))
                    {
                        rabbitCam.SetActive(true);
                        mainCam.SetActive(false);
                        player.SetActive(false);
                        currentState = states.talking;
                        i = 0;
                    }
                    break;
                case states.talking:
                    bg.gameObject.SetActive(true);
                    text.text = dialogLines[i];
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        try
                        {
                            i++; text.text = dialogLines[i];
                        }
                        catch
                        {
                            mainCam.SetActive(true);
                            rabbitCam.SetActive(false);
                            player.SetActive(true);
                            anim.SetInteger("animation", 900);
                            anim.SetBool("combatMode", true);
                            this.GetComponent<BoxCollider>().enabled = false;
                            currentState = states.notTalking;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        mainCam.SetActive(true);
                        rabbitCam.SetActive(false);
                        player.SetActive(true);
                        anim.SetInteger("animation", 900);
                        anim.SetBool("combatMode", true);
                        currentState = states.notTalking;
                    }
                    break;
            }
        }


    }
    public void QuestNum()
    {
        qNum++;
        switch (qNum)
        {
            case 1:
                currentFile = textFile2;
                if (currentFile != null)
                {
                    dialogLines = (currentFile.text.Split('\n'));
                }
                break;
        }
    }
}
