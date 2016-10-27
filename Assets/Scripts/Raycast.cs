using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour 
{

	//private Camera mainCamera;
	// Use this for initialization
	void Start () 
	{
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Vector3 hitpoint = shootRay(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());
		// GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
  //       cube.transform.position = hitpoint;
		 /// Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward) * 50;
       // Debug.DrawRay(mainCamera.transform.position, forward, Color.green);
	}

	public RaycastHit shootRay(Camera mainCamera)
	{
		Vector3 fwd = mainCamera.transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
         
         if (Physics.Raycast(mainCamera.transform.position, fwd, out hit, 100000))
         {
         	return hit;
         }

		return hit;
	}

    public RaycastHit shootSlaveRay(Camera mainCamera, Vector3 angle)
    {
        Vector3 fwd = mainCamera.transform.GetChild(0).TransformDirection(Vector3.forward);
        //print(mainCamera.transform.TransformDirection(Vector3.forward));
        //fwd += angle;
        //print(angle);
        //fwd = new Vector3(0,0,2);
        RaycastHit hit;

        Debug.DrawRay(mainCamera.transform.GetChild(0).position, fwd * 50, Color.green);

        if (Physics.Raycast(mainCamera.transform.GetChild(0).position, fwd, out hit, 100000))
        {
            return hit;
        }

        return hit;
    }
}
