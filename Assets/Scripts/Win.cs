using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collision collider)
    {
        if (collider.gameObject.tag == "AI")
        {
            print("YOU WIN");
            Application.LoadLevel(Application.loadedLevel);
            //fallingAnimation.Play();
            //audioClip.Play();
        }
    }
}
