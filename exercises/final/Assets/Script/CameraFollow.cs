using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position; //Store current camera position in variable temp-temporary position
        temp.x = playerTransform.position.x; //Set the camera's position x to be equal to the player's position x when moving
        temp.y = playerTransform.position.y; //Set the camera's position y to be equal to the player's position y when jumping
        //temp.x += offset; //Add the offset value to the temporary position
        transform.position = temp; //Set back the camera's temp position to the position to the camera's current position

    }
}
