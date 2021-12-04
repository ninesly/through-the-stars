using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustingValue = 1;
    [SerializeField] float rotateValue = 1;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    void Start()
    {
       myRigidbody = GetComponent<Rigidbody>();
       myAudioSource = GetComponent<AudioSource>();
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
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotating(-rotateValue);
        }
        else
        {
            ReleaseConstraintsZRotation();
        }

        if (Input.GetKey(KeyCode.Space)) // thrusting
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustingValue * Time.deltaTime);
            if (!myAudioSource.isPlaying) myAudioSource.Play();
        }
        else
        {
            myAudioSource.Stop();
        }
    }

    private void Rotating(float rotationThisFrame)
    {
        ConstrainZRotation();
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

    private void ReleaseConstraintsZRotation()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void ConstrainZRotation()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }
}
