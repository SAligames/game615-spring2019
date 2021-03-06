﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerConditions : MonoBehaviour
{
    private int score;
    private int lives;
    public Text scoreText;
    public Text lifeText;
    public AudioSource collect;
    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
       if(score>=100)
        {
            lives = lives + 1;
            score = 0;
            SetCountText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            collect.Play();
            SetCountText();
        }
        if (other.gameObject.CompareTag("Green"))
        {
            other.gameObject.SetActive(false);
            score = score + 2;
            collect.Play();
            SetCountText();
        }
        if (other.gameObject.CompareTag("Purple"))
        {
            other.gameObject.SetActive(false);
            score = score + 5;
            collect.Play();
            SetCountText();
        }
        if (other.gameObject.CompareTag("Yellow"))
        {
            other.gameObject.SetActive(false);
            score = score + 10;
            collect.Play();
            SetCountText();
        }
        if (other.gameObject.CompareTag("Pink"))
        {
            other.gameObject.SetActive(false);
            score = score + 25;
            collect.Play();
            SetCountText();
        }
        if(other.gameObject.CompareTag("Death"))
        {
            lives = lives - 1;
            if(lives<0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            SetCountText();
        }

    }


    void SetCountText()
    {
        scoreText.text = "Score:" + score.ToString();
        lifeText.text = "x" + lives.ToString();
    }
}
