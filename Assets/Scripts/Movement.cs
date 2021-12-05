using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // params for tuning
    [Header("Main params:")]
    [SerializeField] float thrustingValue = 1200f;
    [SerializeField] float rotateValue = 200f;

    // special effects
    [Header("Special Effects:")]
    [SerializeField] AudioClip thrustingSound;
    [SerializeField] [Range(0, 1)] float thrustingSoundVolume = 1f;
    [SerializeField] ParticleSystem mainThrusterPartSys;
    [SerializeField] ParticleSystem leftThrusterPartSys;
    [SerializeField] ParticleSystem rightThrusterPartSys;

    // cache
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

    // -------------------------------------------- MOVEMENT

    void ProcessInput()
    {
        // ---------------------------------------------- THRUSTING

        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting(); // just special effects 
        } 


        // ---------------------------------------------- ROTATING

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotating(rotateValue, rightThrusterPartSys); // Left Rotation
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            StartRotating(-rotateValue, leftThrusterPartSys); // Right Rotation
        } 
        else
        {
            StopRotating(); // Release Constraints & VFX stops
        } 
    }

    void StartThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * thrustingValue * Time.deltaTime);

        // special effects
        if (!myAudioSource.isPlaying)
        {
            PlayingSFX(thrustingSound, thrustingSoundVolume, true);
        }
        PlaySpecialEffect(mainThrusterPartSys);
    }

    private void StopThrusting()
    {
        mainThrusterPartSys.Stop();
        myAudioSource.Stop();
    }

    void StartRotating(float rotationThisFrame, ParticleSystem partSysToPlay)
    {
        ConstrainZRotation();
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // special effects
        PlaySpecialEffect(partSysToPlay);
    }

    private void StopRotating()
    {
        ReleaseConstraintsZRotation();

        // special effects
        leftThrusterPartSys.Stop();
        rightThrusterPartSys.Stop();
    }

    // -------------------------------------------- CONSTRAINTS

    void ConstrainZRotation()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void ReleaseConstraintsZRotation()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    // -------------------------------------------- SPECIAL EFFECTS

    void PlaySpecialEffect(ParticleSystem partSysToPlay)
    {
        if (!partSysToPlay.isPlaying)
        {
            partSysToPlay.Play();
        }
    }

    public void PlayingSFX(AudioClip clipToPlay, float volume, bool loop)
    {
        myAudioSource.volume = volume;
        myAudioSource.clip = clipToPlay;
        myAudioSource.loop = loop;
        myAudioSource.Play();
    } // Public!







}
