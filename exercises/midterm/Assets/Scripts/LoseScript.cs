using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{
    void OnTriggerEnter(Collider lose)
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
