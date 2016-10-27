using UnityEngine;
using System.Collections;

public class CopyCamLaser : MonoBehaviour
{
    public Camera camToFollow;

	// Use this for initialization
	void Start ()
    {
        //transform.localPosition = new Vector3(0, 10, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localRotation = camToFollow.transform.localRotation * Quaternion.Euler(10,0,0);
        transform.localPosition = camToFollow.transform.localPosition;
        //transform.localPosition += new Vector3(0, 2, 0);
    }
}
