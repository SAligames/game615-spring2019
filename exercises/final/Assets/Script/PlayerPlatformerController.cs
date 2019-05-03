using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public Material[] emotions;
    public Text lifeText;
    public Text scoreText;
    public GameObject HitBox;
    public Slider angerBar;
    public Slider joyBar;
    public Slider healthBar;
    public Transform player;
    public Transform respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SpriteRenderer myRenderer;
    private int lives;
    private int score;
    private float maxAnger = 100;
    private float maxJoy = 100;
    private float currentAnger;
    private float currentJoy;
    private float maxHealth = 100;
    private float currentHealth;

    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.enabled = true;
        myRenderer.sharedMaterial = emotions[0];
        lives = 3;
        score = 0;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;
        angerBar.value = maxAnger;
        joyBar.value = maxJoy;
        currentAnger = maxAnger;
        currentJoy = maxJoy;
        
        SetCountText();
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();        
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(myRenderer.sharedMaterial==emotions[0] && angerBar.value>0)
            {
                myRenderer.sharedMaterial = emotions[1];
                angerBar.value -= 10f;
                currentAnger = angerBar.value;
            }
            else
            {
                myRenderer.sharedMaterial = emotions[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (myRenderer.sharedMaterial == emotions[0] && joyBar.value>0)
            {
                myRenderer.sharedMaterial = emotions[2];
                joyBar.value -= 20f;
                maxSpeed = 10;
                jumpTakeOffSpeed = 15;
            }
            else
            {
                myRenderer.sharedMaterial = emotions[0];
                maxSpeed = 7;
                jumpTakeOffSpeed = 12;
            }
        }

        if(score>=50)
        {
            lives = lives + 1;
            score = 0;
            SetCountText();
        }

        /*if(Input.GetKeyDown("Fire1"))
        {
            HitBox.SetActive(true);
        }*/

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Death"))
        {
            lives = lives - 1;
            healthBar.value = maxHealth;
            currentHealth = maxHealth;
            angerBar.value = maxAnger;
            joyBar.value = maxJoy;
            currentAnger = maxAnger;
            currentJoy = maxJoy;
            if (lives==0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            if(myRenderer.sharedMaterial=emotions[1])
            {
                myRenderer.sharedMaterial = emotions[0];
            }
            if (myRenderer.sharedMaterial = emotions[2])
            {
                myRenderer.sharedMaterial = emotions[0];
            }
            SetCountText();
        }
        
        if(other.gameObject.CompareTag("Gear"))
        {
            score = score + 1;
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Gold Gear"))
        {
            score = score + 10;
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Tiny Anger"))
        {
            angerBar.value += 25f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);            
        }
        if (other.gameObject.CompareTag("Average Anger"))
        {
            angerBar.value += 50f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Big Anger"))
        {
            angerBar.value += 75f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Large Anger"))
        {
            angerBar.value += 100f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Damage")
        {
            healthBar.value -= 1f;
            currentHealth = healthBar.value;
            if (currentHealth == 0)
            {
                lives = lives - 1;
                healthBar.value = maxHealth;
                currentHealth = maxHealth;
                angerBar.value = maxAnger;
                joyBar.value = maxJoy;
                currentAnger = maxAnger;
                currentJoy = maxJoy;
                SetCountText();
                player.transform.position = respawnPoint.transform.position;
            }
            if (lives == 0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    void SetCountText()
    {
        lifeText.text = lives.ToString();
        scoreText.text = "x" + score.ToString();
    }

}