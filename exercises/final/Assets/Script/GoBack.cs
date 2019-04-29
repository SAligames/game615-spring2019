using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    

    public void OnTriggerEnter2D(Collider2D again)
    {

        player.transform.position = respawnPoint.transform.position;
        
        Debug.Log("You are now falling. Go to checkpoint!");
    }
}
