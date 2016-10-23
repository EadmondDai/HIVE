using UnityEngine;
using System.Collections;

public class CommandReader : MonoBehaviour
{
    public TextMesh outText;
    public string outputText = "";

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void input(string rawInput)
    {
        if (outputText == "")
            outputText = rawInput;
        else
        {
            //priorText;
            outputText = outputText.Insert(outputText.Length, "\n\n" + rawInput);
        }
        outText.text = outputText;

        if (rawInput == "help")
        {
            //outputText += "HOGS";

            outputText = outputText.Insert(outputText.Length, "\n\nCMDS:\n"+ "dir\n" + "datajack\n" + "light");
            outText.text = outputText;
        }

        else if (rawInput == "halp")
        {
            outputText = outputText.Insert(outputText.Length, "\n\nHALP");
            outText.text = outputText;
        }

        else
        {
            outputText = outputText.Insert(outputText.Length, "\nCommand not recognized.");
            outText.text = outputText;
        }

    }
}
