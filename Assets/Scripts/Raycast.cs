using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour 
{
	private GameObject laser;
	float boundY = 52;
	float boundX = 30;
	public Camera camToFollow;
	//private Camera mainCamera;
	// Use this for initialization
	void Start () 
	{

		camToFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

    public RaycastHit shootSlaveRay(Camera mainCamera, float percentZ, float percentY)
    {
    	laser = mainCamera.transform.GetChild(0).gameObject;
        Vector3 fwd = laser.transform.TransformDirection(Vector3.forward);

        float xSubtrac = 0;

        if (percentZ < 0)
        {
        	if (percentY < 0)
        	{
        		xSubtrac = 10 * percentZ ;
        	}
        	else
        	{
        		xSubtrac = 10 * -percentZ;
        	}
        }

         if (percentZ > 0)
        {
        	if (percentY < 0)
        	{
        		xSubtrac = 10 * -percentZ;
        	}

        	else
        	{
        		xSubtrac = 10 * percentZ;
        	}
        	print(xSubtrac);
        }

        laser.transform.localPosition = new Vector3((percentZ * 2), (percentY * 2), 0);
        //laser.transform.localRotation = camToFollow.transform.localRotation * Quaternion.Euler(-45,0,0);
        laser.transform.rotation = Quaternion.Euler((percentY * -boundX) + xSubtrac, percentZ * boundY, 0);
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
