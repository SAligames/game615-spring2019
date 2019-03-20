using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float timePerCycle = 1;
    public float magnitude = 10;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the target with a smooth cosine wave
        var y = magnitude * Mathf.Cos(Time.time / timePerCycle * 2 * Mathf.PI);
        transform.position = initialPosition + Vector3.up * y;
    }
}
