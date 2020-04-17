﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{

    [SerializeField] Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal")<0)
        {
            cam.transform.Translate(new Vector3(-0.1f,0,0));
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            cam.transform.Translate(new Vector3(0.1f, 0, 0));
        }
        if (Input.GetAxis("Vertical")>0)
        {
            cam.transform.Translate(new Vector3(0,0.1f,0));
        }
        else if (Input.GetAxis("Vertical")<0)
        {
            cam.transform.Translate(new Vector3(0,-0.1f,0));
        }
    }
}
