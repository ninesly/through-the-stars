using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustingValue = 1;
    [SerializeField] float rotateValue = 1;

    Rigidbody myRigidbody;

    void Start()
    {
       // myRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, rotateValue));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, -rotateValue));
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, thrustingValue, 0));
        }
    }
}
