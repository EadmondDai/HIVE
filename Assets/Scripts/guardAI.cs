﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class guardAI : MonoBehaviour
{
    public Transform[] targets;
    private AICharacterControl aiCharControl;
    private float minDist = 5;
    int i = 0;

    // Line of sight variables
    private float fov = 60.0f;
    private float sightDist = 20.0f;
    private RaycastHit hit;
    private CommandReader cmdReader;
    public bool lost = false;
    public float startTime = 0;
    public bool stationary = false;


    // Use this for initialization
    void Start()
    {
        cmdReader = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<CommandReader>();
        aiCharControl = transform.GetComponent<AICharacterControl>();

        if (targets.Length > 0)
        {
            i = 0;
            aiCharControl.target = targets[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lost == true)
        {
            if (Time.time-startTime > 2.5)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (stationary == true)
        {
            aiCharControl.target = aiCharControl.transform;
        }

        if (Vector3.Distance(aiCharControl.transform.position, aiCharControl.target.position) < minDist && stationary != true)
        {
           if (i < (targets.Length - 1))
           {
               i++;
               aiCharControl.target = targets[i];
           }
           else
           {
               i = 0;
               aiCharControl.target = targets[i];
           }
        }

        if (cmdReader.slaveCam != null)
        {
            if (LOS(cmdReader.slaveCam.transform.parent.transform) == true)
            {
                cmdReader.moveState = 5;
                cmdReader.noSignal = true;
                if (cmdReader.camView.active == true)
                {
                    cmdReader.camView.active = false;
                    cmdReader.noSignalPlane.active = true;
                }
                cmdReader.username = "";
                cmdReader.stage = 0;
            Destroy(cmdReader.slaveCam.transform.parent.gameObject);
                cmdReader.outputText = cmdReader.outputText.Insert(cmdReader.outputText.Length, "\nYou were spotted...\nYour drone has been taken from you.");
                cmdReader.outText.text = cmdReader.outputText;
                lost = true;
                cmdReader.gameOverSound();
                startTime = Time.time;
            }
        }  
    }

    bool LOS(Transform target)
    {
        if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov && Physics.Linecast(transform.position, target.position, out hit) &&  hit.collider.transform == target && Vector3.Distance(transform.position, target.position) < sightDist)
        {
            return true;
        }

       print(Vector3.Angle(target.position - transform.position, transform.forward) + " : " +  fov + " | " + Physics.Linecast(transform.position, target.position, out hit) + " | " + hit.collider.transform + " : " + target);
        return false;
    }

}