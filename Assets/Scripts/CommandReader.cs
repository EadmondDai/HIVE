using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    public TextMesh outText;
    public string outputText = "";
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
    private float aiTurnSpeed = 1.0f;
    public bool firstPerson = true;
    public bool raycasting = false;
    private string username;
    private int stage = 0;      //when hack into peple, stage = 1
    private Vector3 referencePoint;

    // Sound variables
    public AudioSource laserAudio;
    public AudioClip laserSound;

    // Use this for initialization
    void Start()
    {
        aiTarget = new GameObject();
        aiControl = GameObject.FindGameObjectWithTag("AI").GetComponent<AICharacterControl>();
        navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
        camView = GameObject.FindGameObjectWithTag("ComputerScreen");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        ray = GetComponent<Raycast>();
        rayCube = GameObject.FindGameObjectWithTag("RayCube");
        referencePoint = GameObject.FindGameObjectWithTag("ScreenReference").transform.position;

        slaveCam = aiControl.transform.GetChild(3).GetComponent<Camera>();

        // Initialize sound variables
        laserAudio = mainCam.GetComponent<AudioSource>();
        laserSound = (AudioClip)Resources.Load("Sounds/LaserBeep");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = ray.shootRay(mainCam.GetComponent<Camera>());

        float dist = Vector3.Distance(mainCam.transform.position, hit.point);
        dist = dist / 150;

        if (hit.collider.gameObject.name == "ScreenRender")
        {
            //float angle = Vector3.Angle(hit.point, referencePoint);
            Vector3 angle = referencePoint - hit.point;
            //angle.x = angle.z;
            //angle.x = 0;
            angle.y = -angle.y;
            print(referencePoint + " : " + hit.point + " : " + (referencePoint - hit.point));


            hit = ray.shootSlaveRay(slaveCam, angle);
            dist = Vector3.Distance(slaveCam.transform.position, hit.point);
            dist = dist / 100;
        }
        rayCube.transform.localScale = new Vector3(dist,dist,dist);
        rayCube.transform.position = hit.point;
        rayRend = rayCube.GetComponent<Renderer>();

        if (raycasting == true)
        {
            rayRend.material.shader = Shader.Find("Unlit/Color");
            rayRend.material.SetColor("_Color", Color.red);
            if (Time.time - startTime >= .5f)
            {
                raycasting = false;
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
            moveLeft();
        }
        else if (moveState == 2)
        {
            moveRight();
        }
        else if (moveState == 3)
        {
            moveForward();
        }
        else if (moveState == 4)
        {
            moveBack();
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
    }

    public string getUsername()
    {
        return username;
    }

    public bool matchName(string rawInput)
    {
        if (rawInput == "jack" || rawInput == "tom")
        {
            return true;
        }
        else
            return false;
    }

    public void input(string rawInput)
    {
        outputText = outputText.Insert(outputText.Length, username + "<" + rawInput + "\n");
        outText.text = outputText;
        char[] delimiterChars = { ' ', '.' };
        string[] words = rawInput.ToLower().Split(delimiterChars);
        string cameraCommand = "turn  Rotate the player\n";
        string tabKey = "tab(key)  Switch view\n";



            if (stage == 0)
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

                    outputText = outputText.Insert(outputText.Length, "\n\n" + hackCommand + cameraCommand + tabKey);
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
                        if (matchName(words[1]))
                        {
                            username = words[1];
                            stage = 1;
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
                    outputText = outputText.Insert(outputText.Length, "\n\n" + moveCommand + cameraCommand + exitCommand + tabKey);
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
                        outputText = outputText.Insert(outputText.Length, "\n\n" + "move.north\n" + "move.south\n" + "move.west\n" + "move.east\n" + "stop\n");
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
        navAgent.enabled = false;
    }

    private void moveLeft()
    {
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(-aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveRight()
    {
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(aiSpeed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveForward()
    {
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void moveBack()
    {
        navAgent.enabled = true;
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, -aiSpeed);
        aiControl.target = aiTarget.transform;
    }

    private void turnLeft()
    {
        navAgent.enabled = false;
        aiControl.transform.Rotate(0, -aiTurnSpeed, 0);
    }

    private void turnRight()
    {
        navAgent.enabled = false;
        aiControl.transform.Rotate(0, aiTurnSpeed, 0);
    }
}