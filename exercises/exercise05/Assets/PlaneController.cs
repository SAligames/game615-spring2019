using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    float cameraFollowBehindAmount = 50;
    float cameraFollowAboveAmount = 25;
    int score;
    public Text scoreText;
    private Rigidbody rb;
    public Collider box;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("plane pilot script added to:" + gameObject.name);
        rb = GetComponent<Rigidbody>();
        box = GetComponent<Collider>();
        score = 0;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 90.0f;
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

        Camera.main.transform.position = transform.position + -transform.forward * cameraFollowBehindAmount + Vector3.up * cameraFollowAboveAmount;
        Camera.main.transform.LookAt(transform);

        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        if(terrainHeightWhereWeAre>transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            Destroy(other.gameObject);
            score = score + 1;
            SetCountText();
        }            
        
    }

    void SetCountText()
    {
        scoreText.text = "Score:" + score.ToString();
    }
}
