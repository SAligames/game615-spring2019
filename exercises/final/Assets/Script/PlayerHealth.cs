using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    private float maxHealth = 100;
    private float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = maxHealth;
        currentHealth = maxHealth;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag=="Damage")
        {
            healthBar.value -= 1f;
            currentHealth = healthBar.value;
            if(currentHealth==0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }
}
