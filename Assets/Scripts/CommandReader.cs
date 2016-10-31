using UnityEngine;
using System.Collections;
//using System;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    private int index;  //index of list of gameobjects for erase()
    private int eraseStart = 0;
    public GameObject endText;
    private List<GameObject> list = new List<GameObject>();
    public GameObject[] NPCs;
    public GameObject[] Cameras;
    public GameObject Command;
    public TextMesh outText;
    public string outputText = "";
    public RenderTexture rendTex;
    public AICharacterControl aiControl;
    public NavMeshAgent navAgent;
    private GameObject aiTarget;
    public GameObject camView;
    private GameObject mainCam;
    public GameObject noSignalPlane;
    public Camera slaveCam;
    public int moveState = 0;  // Flag controlling move to left, right, forward, back by 1, 2, 3, 4
    private float aiSpeed = 2.0f;
    private float startTime;
    private float aiTurnSpeed = 30f;
    private float hackVol = 0.07f;
    private float errorVol = 0.1f;
    public bool firstPerson = true;
    public bool showingName = false;
    public bool noSignal = true;
    public string username;
    public int stage = 0;      //when hack into peple, stage = 1
    private Vector3 referencePoint;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;
    private Quaternion initRotation;

    private guardAI ai;

    // Sound variables
    public AudioSource endAudio;
    public AudioClip endSound;
    public AudioClip overSound;
    public AudioClip timerSound;
    public AudioClip hackedSound;
    public AudioClip errorSound;

    // Use this for initialization
    void Start()
    {
        Command = GameObject.FindGameObjectWithTag("Command");
        NPCs = GameObject.FindGameObjectsWithTag("AI");
        Cameras = GameObject.FindGameObjectsWithTag("Camera");
        aiTarget = new GameObject();
        camView = GameObject.FindGameObjectWithTag("ComputerScreen");
        noSignalPlane = GameObject.FindGameObjectWithTag("NoSignal");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        referencePoint = GameObject.FindGameObjectWithTag("ScreenReference").transform.position;

        camView.active = false;
        noSignalPlane.active = false;

        outputText = outputText.Insert(outputText.Length, "\nType HELP for instructions");
        outText.text = outputText;
        endAudio = mainCam.GetComponent<AudioSource>();
        endSound = (AudioClip)Resources.Load("Sounds/Tic");
        overSound = (AudioClip)Resources.Load("Sounds/GameOver2");
        timerSound = (AudioClip)Resources.Load("Sounds/Timer");
        hackedSound = (AudioClip)Resources.Load("Sounds/Hacked");
        errorSound = (AudioClip)Resources.Load("Sounds/Error");
    }

    // Update is called once per frame
    void Update()
    {

        if (eraseStart == 1)
        {
            if (index < list.Count)
            {
                if (list[index].name != "Player" && list[index] != null)
                {
                    if (list[index].GetComponent<MeshRenderer>())
                    {
                        list[index].GetComponent<MeshRenderer>().enabled = false;
                    }
                    //print(index + ": " + list[index].name);
                    //Destroy(list[index]);
                }

                if (index%5==0)
                {
                    endAudio.pitch = Random.Range(1.0f -.1f, 1.0f +.1f);
                    endAudio.PlayOneShot(endSound, 0.175f);
                }

                index++;
            }
            else
            {
                endText.active = true;
            }
        }

        if (showingName == true)
        {
            print("showing");
            if (Time.time - startTime >= 3.5f)
            {
                endAudio.pitch = 1.0f;
                endAudio.Stop();
                for (int i = 0; i < NPCs.Length; i++)
                {
                    print(NPCs[i]);
                    NPCs[i].transform.GetChild(4).gameObject.SetActive(false);
                    NPCs[i].transform.GetChild(5).gameObject.SetActive(false);
                }
                for (int i = 0; i < Cameras.Length; i++)
                {
                    Cameras[i].transform.GetChild(0).gameObject.SetActive(false);
                    Cameras[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                showingName = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
       {
            if (noSignal == false)
            {
                camView.active = !camView.active;
            }
            else
            {
                noSignalPlane.active = !noSignalPlane.active;
            }
        }

        if (moveState == 1)
        {
            moveNorth();
        }
        else if (moveState == 2)
        {
            moveSouth();
        }
        else if (moveState == 3)
        {
            moveWest();
        }
        else if (moveState == 4)
        {
            moveEast();
        }
        else if (moveState == 5)
        {
            stop();
        }
        else if (moveState == 6)
        {
            turnLeft();
        }
        else if (moveState == 7)
        {
            turnRight();
        }
        else if (moveState == 8)
        {
            moveForward();
        }
        else if (moveState == 9)
        {
            moveBack();
        }
        else if (moveState == 10)
        {
            moveLeft();
        }
        else if (moveState == 11)
        {
            moveRight();
        }
        else if (moveState == 12)
        {
            turnUp();
        }
        else if (moveState == 13)
        {
            turnDown();
        }

    }

    public string getUsername()
    {
        return username;
    }

    public GameObject matchName(string rawInput)
    {
        for (int i = 0; i < NPCs.Length; i++) {
            if(rawInput == NPCs[i].name.ToLower())
            {
                return NPCs[i];
            }
        }
        return null;
    }

    public GameObject matchCamera(string rawInput)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (rawInput == Cameras[i].name.ToLower())
            {
                return Cameras[i];
            }
        }
        return null;
    }

    public void input(string rawInput)
    {
        if (rawInput == "")
            rawInput = "stop";

        outputText = outputText.Insert(outputText.Length, "\n\n" + username + "<" + rawInput + "\n");
        outText.text = outputText;
        char[] delimiterChars = { ' ', '.' };
        string[] words = rawInput.ToLower().Split(delimiterChars);
        string cameraCommand = "turn  Rotate the person\n";
        string tabKey = "tab(key)  Turn on/off video-feed\n";
        //string raycastCommand = "raycast  Give info on what you're looking at\n";
        string showNameCommand = "showname  Overlay names of objects in view\n";
        //string lookCommand = "look  Look up and down\n";


        if (words[0] == "showname")
        {
            endAudio.PlayOneShot(timerSound, 0.5f);
           // endAudio.pitch = 1.5f;
            print("showname");

            for (int i = 0; i < NPCs.Length; i++)
            {
                print(NPCs[i]);
                NPCs[i].transform.GetChild(4).gameObject.SetActive(true);
                NPCs[i].transform.GetChild(5).gameObject.SetActive(true);
            }
            for (int i = 0; i < Cameras.Length; i++)
            {
                Cameras[i].transform.GetChild(0).gameObject.SetActive(true);
                Cameras[i].transform.GetChild(1).gameObject.SetActive(true);
            }

            Command.transform.GetChild(4).gameObject.SetActive(true);
            Command.transform.GetChild(5).gameObject.SetActive(true);

            showingName = true;
            startTime = Time.time;
        }

        else if (words[0] == "hack")
        {
            if (words.Length == 1)
            {
                outputText = outputText.Insert(outputText.Length, "\nLack of target name.\n" + "eg.hack jack\n");
                outText.text = outputText;
            }

            else if (words.Length == 2)
            {
                GameObject npc;

                if (words[1].ToLower() == "overseer")
                {
                    //print("CMD");
                    npc = Command;
                    outputText = outputText.Insert(outputText.Length, "\nHacked into overseer \nType HELP to see new commands.");
                        outText.text = outputText;

                    if (slaveCam != null)
                    {
                        stop();
                        moveState = 5;
                        slaveCam.enabled = false;
                    }

                    stop();
                    moveState = 0;
                    username = words[1];
                    stage = 3;
                    ////aiControl = npc.GetComponent<AICharacterControl>();
                    slaveCam = npc.transform.GetChild(3).GetComponent<Camera>();
                    slaveCam.enabled = true;
                    ////ai = aiControl.GetComponent<guardAI>();
                    camView.active = true;
                    noSignalPlane.active = false;
                    noSignal = false;
                    endAudio.pitch = .5f;
                    endAudio.PlayOneShot(hackedSound, hackVol);
                    ////navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
                }


                else
                {
                    npc = matchName(words[1]);

                    if (npc == null)
                    {
                        npc = matchCamera(words[1]);

                        if (npc == null)
                        {
                            outputText = outputText.Insert(outputText.Length, "\nTarget not found.\n");
                            outText.text = outputText;
                        }

                        else
                        {
                            //hack into camera
                            if (slaveCam != null)
                            {
                                stop();
                                moveState = 5;
                                slaveCam.enabled = false;
                            }

                            slaveCam = npc.transform.GetChild(2).GetChild(0).GetComponent<Camera>();
                            slaveCam.enabled = true;
                            camView.active = true;
                            noSignalPlane.active = false;
                            noSignal = false;
                            stage = -1;

                            username = words[1];
                            outputText = outputText.Insert(outputText.Length, "\nHacked into " + words[1] + "\n");
                            outText.text = outputText;
                            endAudio.pitch = 1.0f;
                            endAudio.PlayOneShot(hackedSound, hackVol);
                        }


                    }

                    else
                    {
                        if (slaveCam != null)
                        {
                            stop();
                            moveState = 5;
                            slaveCam.enabled = false;
                        }

                        stop();
                        moveState = 5;
                        username = words[1];
                        stage = 1;
                        aiControl = npc.GetComponent<AICharacterControl>();
                        slaveCam = aiControl.transform.GetChild(3).GetComponent<Camera>();
                        slaveCam.enabled = true;
                        ai = aiControl.GetComponent<guardAI>();
                        camView.active = true;
                        noSignalPlane.active = false;
                        noSignal = false;
                        navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
                        endAudio.pitch = 1.0f;
                        endAudio.PlayOneShot(hackedSound, hackVol);

                        Renderer rend = npc.transform.GetChild(0).GetComponent<Renderer>();
                        rend.material.shader = Shader.Find("Standard");
                        Color color = new Color(0F, 0F,0F, 1F);
                        rend.material.SetColor("_Color", color);

                        outputText = outputText.Insert(outputText.Length, "\nHacked into " + words[1] + "\nHit TAB to turn off/on video-feed.\nType HELP to see new commands.");
                        outText.text = outputText;
                        //aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    }
                }
            }

            else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }
        }

        else if (stage == -1)
        {
            if (words[0] == "help")
            {
                string hackCommand = "hack  Hack an individual\n";

                outputText = outputText.Insert(outputText.Length, "\n\n" + hackCommand + showNameCommand + tabKey);
                outText.text = outputText;
            }

             else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }

        }

        else if (stage == 0)
        {
            if (words[0] == "help")
            {
                string hackCommand = "hack  Hack an individual\n";

                outputText = outputText.Insert(outputText.Length, "\n\n" + hackCommand + showNameCommand + tabKey);
                outText.text = outputText;
            }

            else if (words[0] == "stop")
            {
                moveState = 5;
            }

            else if (words[0] == "turn")
            {
                if (words.Length == 1)
                {
                    outputText = outputText.Insert(outputText.Length, "\n\n" + "turn.left\n" + "turn.right\n" + "stop\n");
                    outText.text = outputText;
                }
                else if (words.Length == 2)
                {
                    if (words[1] == "left")
                    {
                        moveState = 6;
                        stop();
                    }

                    else if (words[1] == "right")
                    {
                        moveState = 7;
                        stop();
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                        endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                    }
                }

                else
                {
                    outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                    outText.text = outputText;
                    endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                }
            }

            else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }
        }

        else if (stage == 1)
        {
            if (words[0] == "help")
            {
                string moveCommand = "move  Move the person\n";
                string turnCommand = "turn  Rotate the person\n";
                string lookCommand = "look  Look up and down\n";
                string exitCommand = "exit  Exit from hacking\n";
                outputText = outputText.Insert(outputText.Length, "\n\n" + moveCommand + turnCommand + lookCommand + cameraCommand + exitCommand + showNameCommand + tabKey);
                outText.text = outputText;
            }

            else if (words[0] == "exit")
            {
                outputText = outputText.Insert(outputText.Length, "\nExit from hacking" + username + "\n");
                outText.text = outputText;
                moveState = 5;


                noSignal = true;
                if (camView.active == true)
                {
                    camView.active = false;
                    noSignalPlane.active = true;
                }
                username = "";
                stage = 0;
            }

            else if (words[0] == "stop")
            {
                moveState = 5;
            }

            else if (words[0] == "move")
            {
                if (words.Length == 1)
                {
                    outputText = outputText.Insert(outputText.Length, "\n\n" + "move.forward\n" + "move.back\n" + "move.left\n" + "move.right\n" + "stop\n");
                    outText.text = outputText;
                }
                else if (words.Length == 2)
                {
                    if (words[1] == "north")
                    {
                        moveState = 1;
                    }

                    else if (words[1] == "south")
                    {
                        moveState = 2;
                    }

                    else if (words[1] == "west")
                    {
                        moveState = 3;
                    }

                    else if (words[1] == "east")
                    {
                        moveState = 4;
                    }

                    else if (words[1] == "forward")
                    {
                        moveState = 8;
                    }

                    else if (words[1] == "back")
                    {
                        moveState = 9;
                        back = -aiControl.transform.forward;
                    }

                    else if (words[1] == "left")
                    {
                        left = -aiControl.transform.right;
                        moveState = 10;
                    }
                    else if (words[1] == "right")
                    {
                        right = aiControl.transform.right;
                        moveState = 11;
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                        endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                    }
                }
                else
                {
                    outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                    outText.text = outputText;
                    endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                }
            }

            else if (words[0] == "turn")
            {
                if (words.Length == 1)
                {
                    outputText = outputText.Insert(outputText.Length, "\n\n" + "turn.left\n" + "turn.right\n" + "stop\n");
                    outText.text = outputText;
                }
                else if (words.Length == 2)
                {
                    if (words[1] == "left")
                    {
                        moveState = 6;
                    }

                    else if (words[1] == "right")
                    {
                        moveState = 7;
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                        endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                    }
                }
            }

            else if (words[0] == "look")
            {
                if (words.Length == 1)
                {
                    outputText = outputText.Insert(outputText.Length, "\n\n" + "look.up\n" + "look.down\n" + "stop\n");
                    outText.text = outputText;
                }
                else if (words.Length == 2)
                {

                    if (words[1] == "up")
                    {
                        moveState = 12;
                        initRotation = aiControl.transform.rotation;
                    }

                    else if (words[1] == "down")
                    {
                        moveState = 13;
                        initRotation = aiControl.transform.rotation;
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                        endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                    }
                }

                else
                {
                    outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                    outText.text = outputText;
                    endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
                }
            }

            else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }
            }

        else if (stage == 3)
        {
            if (words[0] == "help")
            {
                string eraseCommand = "eraseall  Erase everything.\n";
                outputText = outputText.Insert(outputText.Length, "\n\n" +  tabKey + showNameCommand + eraseCommand);
                outText.text = outputText;
            }

            else if (words[0] == "eraseall")
            {
                foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (Vector3.Distance(gameObj.transform.position, mainCam.transform.position) < 45)
                    {
                        list.Add(gameObj);
                    }
                    else if (gameObj.GetComponent<MeshRenderer>())
                    {
                        gameObj.GetComponent<MeshRenderer>().enabled = false;
                    }


                }
                eraseStart = 1;
            }

            else
            {
                print("UHOH");
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }
        }

        else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
                endAudio.pitch = 1.0f;
                endAudio.PlayOneShot(errorSound, errorVol);
            }

        }

    private void stop()
    {
        if (aiControl != null)
        {
            ai.enabled = false;
            aiControl.target = aiControl.transform;
            //navAgent.enabled = false;
            aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        }
    }

    private void moveNorth()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void moveSouth()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, -aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void moveWest()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(-aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveEast()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }


    private void turnLeft()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        //navAgent.enabled = false;
        aiControl.transform.Rotate(0, -aiTurnSpeed * Time.deltaTime, 0);
    }

    private void turnRight()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        // navAgent.enabled = false;
        aiControl.transform.Rotate(0, aiTurnSpeed * Time.deltaTime, 0);
    }

    private void moveForward()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + aiControl.transform.forward * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveBack()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + back * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveRight()
    {
        stop();
    	aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + right * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveLeft()
    {
        stop();
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + left * aiSpeed;
        aiControl.target = aiTarget.transform;
    }
    private void turnUp()
    {
        stop();
        //print(slaveCam.transform.localRotation.x);// * 100);
       // aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ ;
        if ((slaveCam.transform.localRotation.x * 100) > -50)
        {
            slaveCam.transform.localRotation *= Quaternion.Euler(-aiTurnSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            stop();
        }

    }

    private void turnDown()
    {
        stop();
        //aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        // navAgent.enabled = false;
        if (Quaternion.Angle(initRotation, slaveCam.transform.rotation) < 90)
        {
            //print(slaveCam.transform.rotation.x * 100);
            //if (slaveCam.transform.rotation.x * 100 > 85) {
            slaveCam.transform.localRotation *= Quaternion.Euler(aiTurnSpeed * Time.deltaTime, 0, 0);
            //slaveCam.GetComponent<CopyCam>().enabled = false;
        }
        else
        {
            stop();
        }
    }
    
    public void gameOverSound()
    {
        endAudio.pitch = 1.0f;
        endAudio.PlayOneShot(overSound, .1f);
    }
}