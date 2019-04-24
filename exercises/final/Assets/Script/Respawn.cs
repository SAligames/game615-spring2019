using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform checkpoint;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;
            player.transform.rotation = checkpoint.rotation;
        }
    }
}
