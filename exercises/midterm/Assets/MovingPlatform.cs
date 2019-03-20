using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] wayPoints;
    public GameObject Player;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(wayPoints[current].transform.position, transform.position)<WPradius)
        {
            current = Random.Range(0, wayPoints.Length);
            if(current>=wayPoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[current].transform.position, Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==Player)
        {
            Player.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject==Player)
        {
            Player.transform.parent = null;
        }
    }
}
