using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
    private CommandReader cmd;
    public int index;
    private string[] tutorials = { "MSG FROM WH1TER0SE:\nTread lightly,\nthe guards can spot an imposter.", "MSG FROM WH1TER0SE:\nFun, isn't it?\nDon't forget about your look command..." , "MSG FROM WH1TER0SE:\nNice work.\nNow the moment we've been waiting for... " };

	// Use this for initialization
	void Start ()
    {
        cmd = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<CommandReader>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
    void OnCollisionEnter(Collision collider)
    {
        //print("YOU WIN" + Time.time);
        if (collider.gameObject.tag == "AI")
        {
            if (collider.gameObject.transform.GetChild(3) && cmd.slaveCam!=null)
            {
                if (collider.gameObject.transform.GetChild(3) == cmd.slaveCam.transform)
                {
                        cmd.outputText = cmd.outputText.Insert(cmd.outputText.Length, "\n" + tutorials[index] );
                        cmd.outText.text = cmd.outputText;

                    Destroy(gameObject);
                }
            }
           
            //fallingAnimation.Play();
            //audioClip.Play();
        }
    }

   
}
