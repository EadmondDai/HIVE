using UnityEngine;
using System.Collections;
//using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    public GameObject[] NPCs;
    public TextMesh outText;
    public string outputText = "";
    public RenderTexture rendTex;
    public AICharacterControl aiControl;
    public Raycast ray;
    public NavMeshAgent navAgent;
    private GameObject aiTarget;
    private GameObject camView;
    private GameObject mainCam;
    private Camera slaveCam;
    private GameObject rayCube;
    private Renderer rayRend;
    private int moveState = 0;  // Flag controlling move to left, right, forward, back by 1, 2, 3, 4
    private float aiSpeed = 2.0f;
    private float startTime;
    private float aiTurnSpeed = 0.5f;
    public bool firstPerson = true;
    public bool raycasting = false;
    public bool showingName = false;
    private string username;
    private int stage = 0;      //when hack into peple, stage = 1
    private Vector3 referencePoint;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    // Raycast screen variables
    float zPosBound = -0.662f;
    float zNegBound = 1.0717f;
    float yPosBound = -0.764567f;
    float yNegBound = -1.458765f;
    float zHalfWidth;// = 2(zPosBound + 3);
    float yHalfWidth;
    float zPoint;
    float yPoint;
    float percentY;
    float percentZ;
    float midpointY;
    float midpointZ;

    // Sound variables
    public AudioSource laserAudio;
    public AudioClip laserSound;

    // Use this for initialization
    void Start()
    {
        NPCs = GameObject.FindGameObjectsWithTag("AI");
        aiTarget = new GameObject();
        //aiControl = GameObject.FindGameObjectWithTag("AI").GetComponent<AICharacterControl>();
        //navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
        camView = GameObject.FindGameObjectWithTag("ComputerScreen");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        ray = GetComponent<Raycast>();
        rayCube = GameObject.FindGameObjectWithTag("RayCube");
        referencePoint = GameObject.FindGameObjectWithTag("ScreenReference").transform.position;

        //slaveCam = aiControl.transform.GetChild(3).GetComponent<Camera>();

        // Initialize sound variables
        laserAudio = mainCam.GetComponent<AudioSource>();
        laserSound = (AudioClip)Resources.Load("Sounds/LaserBeep");

        zHalfWidth =  System.Math.Abs((zPosBound - zNegBound)/2);
        yHalfWidth =  System.Math.Abs((yPosBound - yNegBound)/2);
        midpointZ = zPosBound + zHalfWidth;
        midpointY = yNegBound + yHalfWidth;

        camView.active = false;

        outputText = outputText.Insert(outputText.Length, "\nType HELP for instructions");
        outText.text = outputText;

        //print(midpointZ);
        //print(midpointY);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = ray.shootRay(mainCam.GetComponent<Camera>());

        float dist = Vector3.Distance(mainCam.transform.position, hit.point);
        dist = dist / 150;

        rayCube.transform.localScale = new Vector3(dist,dist,dist);
        rayCube.transform.position = hit.point;
        rayRend = rayCube.GetComponent<Renderer>();

        if (showingName == true)
        {
            if (Time.time - startTime >= 1.5f)
            {
                for (int i = 0; i < NPCs.Length; i++)
                {
                    NPCs[i].transform.GetChild(4).gameObject.SetActive(false);
                }
                showingName = false;
            }
        }

        else
        {
            rayRend.material.shader = Shader.Find("Unlit/Color");
            rayRend.material.SetColor("_Color", Color.white);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
       {
            camView.active = !camView.active;

            if (firstPerson == true)
            {
                mainCam.active = !mainCam.active;
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
            print("left" + Time.time);
            moveLeft();
        }
        else if (moveState == 11)
        {
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
            }
            showingName = true;
            startTime = Time.time;
            //RaycastHit hit = ray.shootRay(mainCam.GetComponent<Camera>());
            //raycasting = false;
            //outputText = outputText.Insert(outputText.Length, "\n\nRaycast hit! \nVector3 = " + hit.point + "\nName: " + hit.collider.gameObject.name);
            //outText.text = outputText;
            //raycasting = true;
            //startTime = Time.time;
            //laserAudio.PlayOneShot(laserSound);
        }

        else if (stage == 0)
            {
            if (words[0] == "raycast")
                {
                    RaycastHit hit = ray.shootRay(mainCam.GetComponent<Camera>());
                    raycasting = false;
                    outputText = outputText.Insert(outputText.Length, "\n\nRaycast hit! \nVector3 = " + hit.point + "\nName: " + hit.collider.gameObject.name);
                    outText.text = outputText;
                    raycasting = true;
                    startTime = Time.time;
                    laserAudio.PlayOneShot(laserSound);
                 }

                else if (words[0] == "help")
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
                        if (npc != null)
                        {
                            username = words[1];
                            stage = 1;
                            aiControl = npc.GetComponent<AICharacterControl>();
                            slaveCam = aiControl.transform.GetChild(3).GetComponent<Camera>();
                            slaveCam.enabled = true;
                            camView.active = true;
                            navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
                            

                        }

                        else
                        {
                            outputText = outputText.Insert(outputText.Length, "\nTarget not found.\n");
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
                if (words[0] == "raycast")
                {
                    RaycastHit hit = ray.shootRay(mainCam.GetComponent<Camera>());
                    raycasting = false;
                    outputText = outputText.Insert(outputText.Length, "\n\nRaycast hit! \nVector3 = " + hit.point + "\nName: " + hit.collider.gameObject.name);
                    outText.text = outputText;
                    raycasting = true;
                    startTime = Time.time;
                    laserAudio.PlayOneShot(laserSound);
                 }

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

                    else if (words[1] == "right")
                    {
                        left = aiControl.transform.right;
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
        aiControl.target = aiControl.transform;
        //navAgent.enabled = false;
        aiControl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
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