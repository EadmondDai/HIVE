using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    public TextMesh outText;
    public string outputText = "";
    public AICharacterControl aiControl;
    public NavMeshAgent navAgent;
    private GameObject aiTarget;
    private GameObject camView;
    private GameObject mainCam;
    private int moveState = 0;  // Flag controlling move to left, right, forward, back by 1, 2, 3, 4
    private float aiSpeed = 2.0f;
    private float aiTurnSpeed = 1.0f;
    private bool firstPerson = true;

    // Use this for initialization
    void Start()
    {
        aiTarget = new GameObject();
        aiControl = GameObject.FindGameObjectWithTag("AI").GetComponent<AICharacterControl>();
        navAgent = aiControl.transform.GetComponent<NavMeshAgent>();
        camView = GameObject.FindGameObjectWithTag("ComputerScreen");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
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

    public void input(string rawInput)
    {
        outputText = outputText.Insert(outputText.Length, "\n\n" + rawInput);
        outText.text = outputText;

        if (rawInput == "help")
        {
            outputText = outputText.Insert(outputText.Length, "\n\ncmd\n" + "dir\n" + "datajack\n" + "light" + "exit");
            outText.text = outputText;
        }

        if (rawInput == "move.back")
        {
            moveState = 1;
        }
        else if (rawInput == "move.forward")
        {
            moveState = 2;
        }
        else if (rawInput == "move.left")
        {
            moveState = 3;
        }
        else if (rawInput == "move.right")
        {
            moveState = 4;
        }
        else if (rawInput == "stop")
        {
            moveState = 5;
        }
        else if (rawInput == "turn.left")
        {
            moveState = 6;
        }
        else if (rawInput == "turn.right")
        {
            moveState = 7;
        }

        else if (rawInput == "light")
        {
            outputText = outputText.Insert(outputText.Length, "\n\ncmd\n" + "dir\n" + "datajack\n" + "light");
            outputText = outputText.Insert(outputText.Length, "\n\nlight.on\n" + "light.off");
            outText.text = outputText;
        }

        else if (rawInput == "exit")
        {
            outputText = outputText.Insert(outputText.Length, "\n\nexit");
            outText.text = outputText;
        }

        else
        {
            outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.");
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