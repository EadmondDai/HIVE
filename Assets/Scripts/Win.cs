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
        //print("YOU WIN" + Time.time);
        if (collider.gameObject.tag == "AI")
        {
            print("1");
            if (collider.gameObject.transform.GetChild(3) && cmd.slaveCam!=null)
            {
                print("2");
                if (collider.gameObject.transform.GetChild(3) == cmd.slaveCam.transform)
                {
                    print("3");
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
        Time.timeScale = 0;
    }
}
