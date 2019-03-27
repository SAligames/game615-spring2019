using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource lifeLost;

    public void OnTriggerEnter(Collider other)
    {

        player.transform.position = respawnPoint.transform.position;
        lifeLost.Play();
        Debug.Log("You are now falling. Go to checkpoint!");
    }
}
