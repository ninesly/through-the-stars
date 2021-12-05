using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float boosterValue = 1f;
    [SerializeField] float rotateValue = 1f;
    [SerializeField] AudioClip boosterSound;
    [SerializeField] [Range(0, 1)] float boosterSoundVolume = 1f;

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

        if (Input.GetKey(KeyCode.Space)) // boosting
        {
            myRigidbody.AddRelativeForce(Vector3.up * boosterValue * Time.deltaTime);
            if (!myAudioSource.isPlaying)
            {
                PlayingSFX(boosterSound, boosterSoundVolume, true);
                myAudioSource.Play();
            }
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


    public void PlayingSFX(AudioClip clipToPlay, float volume, bool loop)
    {
        myAudioSource.volume = volume;
        myAudioSource.clip = clipToPlay;
        myAudioSource.loop = loop;
        myAudioSource.Play();
    }
}
