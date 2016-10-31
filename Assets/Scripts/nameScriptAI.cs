using UnityEngine;
using System.Collections;

public class nameScriptAI : MonoBehaviour
{
    private TextMesh nameMesh;
    private Transform player;
    private CommandReader cmd;
    private float divider = 300;
    private float thresh = 70;
    // Use this for initialization
    void Start ()
    {
        nameMesh = transform.GetChild(0).GetComponent<TextMesh>();
        nameMesh.text = transform.parent.name;
        cmd = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<CommandReader>();
        //player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (cmd.slaveCam != null)
            player = cmd.slaveCam.transform;

        if (player != null)
        {
            transform.LookAt(player);
            float dist = Vector3.Distance(player.position, transform.position);

            if (dist < thresh && player.parent != transform.parent && player.parent.parent != transform.parent)
            {
                print(player.parent + " : " + transform.parent);
                nameMesh.text = transform.parent.name;
                nameMesh.transform.localScale = new Vector3(dist / divider, dist / divider, dist / divider);
            }
            else
            {
                nameMesh.text = "";
            }
        }
	}
}
