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
    public GameObject HitBox;    

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SpriteRenderer myRenderer;
    private int lives;

    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.enabled = true;
        myRenderer.sharedMaterial = emotions[0];
        lives = 3;
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
            if(myRenderer.sharedMaterial==emotions[0])
            {
                myRenderer.sharedMaterial = emotions[1];
            }
            else
            {
                myRenderer.sharedMaterial = emotions[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (myRenderer.sharedMaterial == emotions[0])
            {
                myRenderer.sharedMaterial = emotions[2];
                maxSpeed = 10;
            }
            else
            {
                myRenderer.sharedMaterial = emotions[0];
                maxSpeed = 7;
            }
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
            if(lives==0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            SetCountText();
        }        
    }

    void SetCountText()
    {
        lifeText.text = "Lives:"+ lives.ToString();
    }
}