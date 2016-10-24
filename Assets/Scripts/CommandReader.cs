using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class CommandReader : MonoBehaviour
{
    public TextMesh outText;
    public string outputText = "";
    public AICharacterControl aiControl;
    private GameObject aiTarget;
    private int moveState = 0;  // Flag controlling move to left, right, forward, back by 1, 2, 3, 4
    private float speed = 0.5f;

    // Use this for initialization
    void Start()
    {
        aiTarget = new GameObject();
        aiControl = GameObject.FindGameObjectWithTag("AI").GetComponent<AICharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (rawInput == "move.left")
        {
            moveState = 1;
        }
        else if (rawInput == "move.right")
        {
            moveState = 2;
        }
        else if (rawInput == "move.forward")
        {
            moveState = 3;
        }
        else if (rawInput == "move.back")
        {
            moveState = 4;
        }
        else if (rawInput == "stop")
        {
            moveState = 5;
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
    }

    private void moveLeft()
    {
        aiTarget.transform.position = aiControl.transform.position + new Vector3(-speed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveRight()
    {
        aiTarget.transform.position = aiControl.transform.position + new Vector3(speed, 0, 0);
        aiControl.target = aiTarget.transform;
    }

    private void moveForward()
    {
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, speed);
        aiControl.target = aiTarget.transform;
    }

    private void moveBack()
    {
        aiTarget.transform.position = aiControl.transform.position + new Vector3(0, 0, -speed);
        aiControl.target = aiTarget.transform;
    }
}