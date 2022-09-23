using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTankMovement : MonoBehaviour
{
    public Vector2[] points;
    public int currentPoint;

    public float speed;

    public float health = 100f;

    private SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;
    private GameObject newBullet;
    public float bulletSpeed;

    private float timer;
    public float timerMax = 80;

    public float damage = 50f;
    public float impulse = 1f;

    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        Vector2 position = transform.position;

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
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint], speed);

    }

    private void FixedUpdate()
    {
        if(currentPoint == 0)
        {          
            animator.Play("Tank_Right");
        }
        else if (currentPoint == 1)
        {
            animator.Play("Tank");
        }
       
        timer--;

        if (timer <= 0)
        {
            timer = timerMax;
            if (currentPoint == 0)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.x += 4.5f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
                spawnPosition.y += 0.5f;

                GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                newBullet.transform.position += transform.right;
                newBullet.GetComponent<SpriteRenderer>().flipX = true;
                newBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 5);
            }
            else if (currentPoint == 1)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.x -= 4.5f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
                spawnPosition.y += 0.5f;

                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
                newBullet.transform.position += -transform.right;
                newBullet.GetComponent<SpriteRenderer>().flipX = false;
                newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 5);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.up * impulse, ForceMode2D.Impulse);            
        }
    }
}
