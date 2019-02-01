using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{

    public Material highlightMaterial;
    Material original;
    public float highlightTime = 5;
    float highlightExpireTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        original = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > highlightExpireTime)
        {
            // Reset the original material.
            GetComponent<Renderer>().material = original;
        }
    }
    public void Highlighter()
    {
        GetComponent<Renderer>().material = highlightMaterial;
        highlightExpireTime = Time.time + highlightTime;
    }
}
