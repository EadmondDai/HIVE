using UnityEngine;
using System.Collections;

public class nameScript : MonoBehaviour
{
    private TextMesh nameMesh;
    private Transform player;
	// Use this for initialization
	void Start ()
    {
        nameMesh = transform.GetChild(0).GetComponent<TextMesh>();
        nameMesh.text = transform.parent.name;
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(player);
	}
}
