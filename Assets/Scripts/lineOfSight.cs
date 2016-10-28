using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class lineOfSight : MonoBehaviour {

	private float fov = 60.0f;
	private float sightDist = 20.0f;
	private RaycastHit hit;
	private GameObject player;
	public GameObject playerCam; 
	//public Hide hide;
	public GameObject talkText;
	private AICharacterControl Target;
	private Animator animator;
//private myAI ai;
	//private exclamation exc;

	bool LOS(Transform target)
	{
		if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov &&
            Physics.Linecast(transform.position, target.position, out hit) &&
            hit.collider.transform == target && Vector3.Distance(transform.position,player.transform.position) < sightDist) 
		{
        	return true;
   	 	}

    	return false;
	}

	// Use this for initialization
	void Start () 
	{
		//exc.GetChild(0).GetComponent<exclamation>();
	//	ai = GetComponent<myAI>();
		//player = GameObject.FindGameObjectWithTag("Player");
		//playerCam = player.transform.GetChild(0).gameObject;
		animator = transform.GetChild(0).GetComponent<Animator>();
		Target = transform.GetComponent<AICharacterControl>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (LOS(player.transform) == true && Vector3.Distance(player.transform.position, transform.position) < 5 )
		//{
		//	Target.target = transform;
		//	//hide.Seen(transform);
		//}
		
	
	}
}