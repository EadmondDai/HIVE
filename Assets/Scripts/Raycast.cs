using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour 
{

	private Camera mainCamera;
	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Vector3 hitpoint = shootRay(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());
		// GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
  //       cube.transform.position = hitpoint;
		  Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward) * 50;
        Debug.DrawRay(mainCamera.transform.position, forward, Color.green);
	}

	public Vector3 shootRay(Camera mainCamera)
	{
		Vector3 fwd = mainCamera.transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
         
         if (Physics.Raycast(mainCamera.transform.position, fwd, out hit, 100000))
         {
         	print("YES" + Time.deltaTime);
         	return hit.point;
         }

		return Vector3.zero;
	}
}
