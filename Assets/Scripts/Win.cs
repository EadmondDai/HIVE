using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
    private CommandReader cmd;

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
        print("SNAK ATAK" + Time.time);
        if (collider.gameObject.tag == "AI")
        {
            if (collider.gameObject.transform.GetChild(4) && cmd.slaveCam!=null)
            {
                if (collider.gameObject.transform.GetChild(4) == cmd.slaveCam)
                {
                    win();
                }
            }
           
            //fallingAnimation.Play();
            //audioClip.Play();
        }
    }

    void win()
    {
        print("YOU WIN");
        Application.LoadLevel(Application.loadedLevel);
    }
}
