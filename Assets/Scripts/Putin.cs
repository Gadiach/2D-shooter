using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Putin : MonoBehaviour
{
    public Vector2[] points;
    public int currentPoint;
    
    public int stage;

    private Vector2 currentTarget;

    public float speed;
    public float speedAttack;
    public float speedPatrol;

    public float distanceToAttack;

    private Vector3 spawnPosition;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private Transform player;
    public float damage = 50f;
    
    public float healthMax;
    public float health;
    public Image healthImage;


    private Rigidbody2D rb2d;

    private void Start()
    {
        spawnPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();

        rb2d = GetComponent<Rigidbody2D>();
      
    }

    private void Update()
    {
        Vector2 position = transform.position;

        currentTarget = points[currentPoint];
        if (health > 0)
        {
            if (stage == 0)
            {
                speed = speedPatrol;
                if (position == points[currentPoint])
                {
                    if (currentPoint == 0)
                    {
                        currentPoint = 1;
                    }
                    else if (currentPoint == 1)
                    {
                        currentPoint = 0;
                    }
                }
                if (Vector2.Distance(transform.position, player.position) <= distanceToAttack)
                {
                    stage = 1;
                }
            }
            else if (stage == 1)
            {
                speed = speedAttack;

                currentTarget = player.position;

                if (Vector2.Distance(transform.position, player.position) >= distanceToAttack + 3)
                {
                    stage = 0;
                }
            }
            else if (stage == 2)
            {
                speed = speedAttack;

                currentTarget = spawnPosition;

                if (position == currentTarget)
                {
                    stage = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            if (position.x > currentTarget.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (health <= 0)
        {
            Vector3 temp = transform.position;
            temp.y = 0.8f;
            transform.position = temp;
            animator.Play("Putin_Dead");
        }
    }

    private void LateUpdate()
    {
        health = Mathf.Clamp(health, 0, healthMax);

        healthImage.fillAmount = health / healthMax;

        if (health <= 0)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        }                              
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
            stage = 2;
        }       
    }
}
 