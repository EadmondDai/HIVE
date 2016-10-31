using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
    private CommandReader cmd;
    public int index;
    private string[] tutorials = { "\n\n-MSG FROM WH1TER0SE:\nTread lightly,\nthe guards can spot an imposter.", "\n\n-MSG FROM WH1TER0SE:\nFun, isn't it?\nDon't forget about your look command..." , "\n\n-MSG FROM WH1TER0SE:\nNice work.\nNow the moment we've been waiting for... " };
    private AudioSource audioSource;
    private AudioClip gameSound;

    private float startTime;

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
                        audioSource.pitch = 1.0f;
                        audioSource.PlayOneShot(gameSound, 0.05f);
                        cmd.outputText = cmd.outputText.Insert(cmd.outputText.Length, "\n<color=\"#00FFA8\">" + tutorials[index] + "</color>" );
                        cmd.outText.text = cmd.outputText;
                        startTime = Time.time;

                    Destroy(gameObject);
                }
            }
           
            //fallingAnimation.Play();
            //audioClip.Play();
        }
    }

   
}
