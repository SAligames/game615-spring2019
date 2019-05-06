using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Box"))
        {
            Destroy(col.gameObject);
        }
    }
}
