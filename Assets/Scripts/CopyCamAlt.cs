﻿using UnityEngine;
using System.Collections;

public class CopyCamAlt : MonoBehaviour
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
        transform.localRotation = camToFollow.transform.localRotation;
        transform.localPosition = camToFollow.transform.localPosition;
       // transform.localPosition += new Vector3(0, 1, 0);
    }
}
