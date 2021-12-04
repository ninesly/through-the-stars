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
       myRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotating(rotateValue);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotating(-rotateValue);
        }

        if (Input.GetKey(KeyCode.Space)) // thrusting
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustingValue * Time.deltaTime);
        }
    }

    private void Rotating(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}
