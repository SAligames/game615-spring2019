﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController cc;
    public float speed = 10f;
    float ySpeed = 0;
    float gravity = -15f;
    public Transform fpsCamera;
    float pitch = 0f;

    [Range(5, 15)]
    public float mouseSensitivity = 10f;

    [Range(45, 85)]
    public float pitchRange = 45;

    float xInput = 0f;
    float zInput = 0f;
    float xMouse = 0f;
    float yMouse = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateMovement();        
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal") * speed;
        zInput = Input.GetAxis("Vertical") * speed;
        xMouse = Input.GetAxis("Mouse X") * mouseSensitivity;
        yMouse = Input.GetAxis("Mouse Y") * mouseSensitivity;
    }

    void UpdateMovement()
    {
        Vector3 movement = new Vector3(xInput, 0, zInput);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformVector(movement);


        if (cc.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = 15f;

            }
            else
            {
                ySpeed = gravity * Time.deltaTime;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        //cc.Move(new Vector3(xInput, ySpeed, zInput)*Time.deltaTime);
        cc.Move((movement + new Vector3(0, ySpeed, 0)) * Time.deltaTime);


        transform.Rotate(0, xMouse, 0);


        pitch -= yMouse;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange);
        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);
        fpsCamera.localRotation = camRotation;
    }
}
