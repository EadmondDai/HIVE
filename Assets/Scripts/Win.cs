using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
    private CommandReader cmd;
    public int index;
    private string[] tutorials = { "----------------\n-- MSG FROM WH1TER0SE:\nTread lightly,\nthe guards can spot an imposter.", "MSG FROM WH1TER0SE:\nFun, isn't it?\nDon't forget about your look command..." , "MSG FROM WH1TER0SE:\nNice work.\nNow the moment we've been waiting for... " };
    private AudioSource audioSource;
    private AudioClip gameSound;

	// Use this for initialization
	void Start ()
    {
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        gameSound = (AudioClip)Resources.Load("Sounds/Message");
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
                        audioSource.PlayOneShot(gameSound, 0.4f);
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
