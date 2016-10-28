using UnityEngine;
using System.Collections;
//using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject[] Cameras;
    public TextMesh outText;
    public string outputText = "";
    public RenderTexture rendTex;
    public AICharacterControl aiControl;
    public NavMeshAgent navAgent;
    private GameObject aiTarget;
    private GameObject camView;
    private GameObject mainCam;
    private GameObject noSignalPlane;
    public Camera slaveCam;
    private int moveState = 0;  // Flag controlling move to left, right, forward, back by 1, 2, 3, 4
    private float aiSpeed = 2.0f;
    private float startTime;
    private float aiTurnSpeed = 0.5f;
    public bool firstPerson = true;
    public bool showingName = false;
    public bool noSignal = true;
    private string username;
    private int stage = 0;      //when hack into peple, stage = 1
    private Vector3 referencePoint;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    // Sound variables
    public AudioSource laserAudio;
    public AudioClip laserSound;

    // Use this for initialization
    void Start()
    {
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

        //print(midpointZ);
        //print(midpointY);
    }

    // Update is called once per frame
    void Update()
    {

        if (showingName == true)
        {
            if (Time.time - startTime >= 1.5f)
            {
                for (int i = 0; i < NPCs.Length; i++)
                {
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
        	print("left" + Time.time);
            moveRight();
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
        outputText = outputText.Insert(outputText.Length, username + "<" + rawInput + "\n");
        outText.text = outputText;
        char[] delimiterChars = { ' ', '.' };
        string[] words = rawInput.ToLower().Split(delimiterChars);
        string cameraCommand = "turn  Rotate the player\n";
        string tabKey = "tab(key)  Switch view\n";
        //string raycastCommand = "raycast  Give info on what you're looking at\n";
        string showNameCommand = "showname  Show names of all visible objects\n";


        if (words[0] == "showname")
        {

            for (int i = 0; i < NPCs.Length; i++)
            {
                NPCs[i].transform.GetChild(4).gameObject.SetActive(true);
                NPCs[i].transform.GetChild(5).gameObject.SetActive(true);
            }
            for (int i = 0; i < Cameras.Length; i++)
            {
                Cameras[i].transform.GetChild(0).gameObject.SetActive(true);
                Cameras[i].transform.GetChild(1).gameObject.SetActive(true);
            }
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
                        stage = 1;

                        username = words[1];
                        outputText = outputText.Insert(outputText.Length, "\nHacked into " + words[1] + "\n");
                        outText.text = outputText;
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

                    username = words[1];
                    stage = 1;
                    aiControl = npc.GetComponent<AICharacterControl>();
                    slaveCam = aiControl.transform.GetChild(3).GetComponent<Camera>();
                    slaveCam.enabled = true;
                    camView.active = true;
                    noSignalPlane.active = false;
                    noSignal = false;
                    navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
                }
            }

            else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
            }
        }

        else if (stage == 0)
            {
            if (words[0] == "help")
                {
                    string hackCommand = "hack  hack the person\n";

                    outputText = outputText.Insert(outputText.Length, "\n\n" + hackCommand + tabKey + showNameCommand);
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
                        }

                        else if (words[1] == "right")
                        {
                            moveState = 7;
                        }

                        else
                        {
                            outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                            outText.text = outputText;
                        }
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                    }
                }

                else
                {
                    outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                    outText.text = outputText;
                }
            }

            else if (stage == 1)
            {
                if (words[0] == "help")
                {
                    string moveCommand = "move  Move the person\n";
                    string exitCommand = "exit  Exit from hacking\n";
                    outputText = outputText.Insert(outputText.Length, "\n\n" + moveCommand + cameraCommand + exitCommand + tabKey + showNameCommand);
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
                        outputText = outputText.Insert(outputText.Length, "\n\n" + "move.north\n" + "move.south\n" + "move.west\n" + "move.east\n" + "move.forward\n" + "stop\n");
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
                        //print("moving");
                        else if (words[1] == "right")
                        {
                            right = aiControl.transform.right;
                            moveState = 11;
                        }

                        else
                        {
                            outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                            outText.text = outputText;
                        }
                    }
                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
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
                        }
                    }

                    else
                    {
                        outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                        outText.text = outputText;
                    }
                }
            }

            else
            {
                outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.\n");
                outText.text = outputText;
            }

    }

private void stop()
    {
        if (aiControl != null)
        {
            aiControl.target = aiControl.transform;
            //navAgent.enabled = false;
            aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        }
    }

    private void moveNorth()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void moveSouth()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, -aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void moveWest()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(-aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveEast()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }


    private void turnLeft()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //navAgent.enabled = false;
        aiControl.transform.Rotate(0, -aiTurnSpeed, 0);
    }

    private void turnRight()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
       // navAgent.enabled = false;
        aiControl.transform.Rotate(0, aiTurnSpeed, 0);
    }

    private void moveForward()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + aiControl.transform.forward * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveBack()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + back * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveRight()
    {
    	aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + right * aiSpeed;
        aiControl.target = aiTarget.transform;
    }

    private void moveLeft()
    {
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + left * aiSpeed;
        aiControl.target = aiTarget.transform;
    }
}