using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneShip : MonoBehaviour
{
    public Transform target;
    public int[] speed;
    public int CutIndex;

    public GameObject Player;
    public GameObject Player0;
    public GameObject Playercam;
    public GameObject[] particles;
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        CutIndex = 0;
        LevelManager.instance.IsCutscene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= -80 && transform.position.x <= -10)
        {
            IndexControl(1);
        }
        else if (transform.position.x > -10 && transform.position.x <= -5)
        {
            IndexControl(2);
        }
        else if (transform.position.x > -5)
        {
            IndexControl(3);
        }


        transform.LookAt(target);
        switch (CutIndex) 
        {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed[0] * Time.deltaTime);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed[1] * Time.deltaTime);
                break;

            case 2:
                foreach (GameObject obj in particles)
                {
                    obj.SetActive(false);
                }
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed[2] * Time.deltaTime);
                break;

            case 3:
                Player0.SetActive(false);
                fade.GetComponent<Animator>().SetTrigger("Go");
                Invoke("Fadeoff", 6);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed[3] * Time.deltaTime);
                break;
        }

        if (transform.position == target.position)
        {
            Player.SetActive(true);
            Playercam.SetActive(true);
            LevelManager.instance.DialoguePanel.SetActive(true);
        }
    }

    void Fadeoff()
    {
        fade.SetActive(false);
    }

    public void IndexControl(int i)
    {
        CutIndex = i;

    }
}
