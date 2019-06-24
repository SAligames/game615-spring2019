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
    public Slider angerBar;
    public Slider joyBar;
    public Slider healthBar;
    public Transform player;
    public Transform respawnPoint;
    public Collider2D attackTrigger;
    public Collider2D secondAttackTrigger;
    public AudioSource collect;

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
    private bool attack = false;
    private float attackTimer = 5f;
    private float attackCD = 0.3f;

    protected float strength = 3;

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
        attackTrigger.enabled = false;
        secondAttackTrigger.enabled = false;
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
                angerBar.value -= 25f;
                currentAnger = angerBar.value;
                strength = 50;
            }
            else if(myRenderer.sharedMaterial==emotions[2] && angerBar.value>0)
            {
                myRenderer.sharedMaterial = emotions[1];
                angerBar.value -= 25f;
                currentAnger = angerBar.value;
                strength = 50;
            }            
            else
            {
                myRenderer.sharedMaterial = emotions[0];
                strength = 3;
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
            else if (myRenderer.sharedMaterial == emotions[1] && joyBar.value > 0)
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

        if(Input.GetKeyDown(KeyCode.Mouse0) && !attack)
        {
            attack = true;
            attackTimer = attackCD;
            attackTrigger.enabled = true;
            secondAttackTrigger.enabled = true;
        }
        
        if(attack)
        {
            if(attackTimer>0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attack = false;
                attackTrigger.enabled = false;
                secondAttackTrigger.enabled = false;
            }
        }
        
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetBool("attack", attack);

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
            else  if (myRenderer.sharedMaterial = emotions[2])
            {
                myRenderer.sharedMaterial = emotions[0];
            }
            SetCountText();
        }
        
        if(other.gameObject.CompareTag("Gear"))
        {
            score = score + 1;
            other.gameObject.SetActive(false);
            collect.Play();
            SetCountText();
        }

        if (other.gameObject.CompareTag("Gold Gear"))
        {
            score = score + 10;
            other.gameObject.SetActive(false);
            collect.Play();
            SetCountText();
        }

        if (other.gameObject.CompareTag("Tiny Anger"))
        {
            angerBar.value += 25f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Average Anger"))
        {
            angerBar.value += 50f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Big Anger"))
        {
            angerBar.value += 75f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Large Anger"))
        {
            angerBar.value += 100f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }

        if (other.gameObject.CompareTag("Tiny Joy"))
        {
            joyBar.value += 25f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Average Joy"))
        {
            joyBar.value += 50f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Big Joy"))
        {
            joyBar.value += 75f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
        }
        if (other.gameObject.CompareTag("Large Joy"))
        {
            joyBar.value += 100f;
            //currentAnger = angerBar;
            other.gameObject.SetActive(false);
            collect.Play();
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
                if (myRenderer.sharedMaterial = emotions[1])
                {
                    myRenderer.sharedMaterial = emotions[0];
                }
                else if (myRenderer.sharedMaterial = emotions[2])
                {
                    myRenderer.sharedMaterial = emotions[0];
                }
            }
            if (lives == 0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Light Box") && strength >= 3)
        {
            Destroy(col.gameObject);
        }

        if(col.gameObject.CompareTag("Heavy Box") && strength >= 50)
        {
            Destroy(col.gameObject);
        }
    }

    void SetCountText()
    {
        lifeText.text = lives.ToString();
        scoreText.text = "x" + score.ToString();
    }

}