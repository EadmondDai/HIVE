using UnityEngine;
using System.Collections;

public class nameScript : MonoBehaviour
{
    private TextMesh nameMesh;
    private Transform player;
    private float divider = 500;
    private float thresh = 100;
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
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < thresh)
        {
            nameMesh.text = transform.parent.name;
            nameMesh.transform.localScale = new Vector3(dist / divider, dist / divider, dist / divider);
        }
        else
        {
            nameMesh.text = "";
        }
    }
}
