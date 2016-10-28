using UnityEngine;
using System.Collections;

public class TalkText : MonoBehaviour {

public GameObject player;
	// Use this for initialization
	void Start () 
	{
		player= GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	transform.LookAt(player.transform);
	}
}
