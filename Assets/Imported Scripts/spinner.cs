﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour {

    [SerializeField] float Revolutions = 1f;
    //[SerializeField] Vector3 SpinVect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward * Revolutions);

	}
}