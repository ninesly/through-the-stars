using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;    
    [SerializeField] float period = 2f;

    float movementFactor = 0f;
    Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;        
    }
    void Update()
    {
        Oscilate();
    }

    void Oscilate()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.28..
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalc. to go from 0 to 1

        var offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
