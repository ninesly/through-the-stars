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
    [SerializeField] ParticleSystem rightThrusterPartSys;
    [SerializeField] ParticleSystem leftThrusterPartSys;
    [SerializeField] ParticleSystem mainThrusterPartSys;

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

            // special effects
            if (!rightThrusterPartSys.isPlaying)
            {
                rightThrusterPartSys.Play();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotating(-rotateValue);

            // special effects
            rightThrusterPartSys.Stop();
            if (!leftThrusterPartSys.isPlaying)
            {
                leftThrusterPartSys.Play();
            }
        }
        else
        {
            ReleaseConstraintsZRotation();

            // special effects
            leftThrusterPartSys.Stop();
            rightThrusterPartSys.Stop();            
        }

        if (Input.GetKey(KeyCode.Space)) // boosting
        {
            myRigidbody.AddRelativeForce(Vector3.up * boosterValue * Time.deltaTime);
            if (!myAudioSource.isPlaying)
            {
                PlayingSFX(boosterSound, boosterSoundVolume, true);
                myAudioSource.Play();
            }
            if (!mainThrusterPartSys.isPlaying)
            {
                mainThrusterPartSys.Play();
            }            
        }
        else
        {
            mainThrusterPartSys.Stop();
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
