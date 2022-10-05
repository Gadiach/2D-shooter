using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helikopter : MonoBehaviour
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

    private bool isAlive = true;

    public float shootAmount;


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
        Vector3 helicopterScale = transform.localScale;

        if (currentPoint == 0)
        {
            animator.Play("Helicopter_Right");
        }
        else if (currentPoint == 1)
        {
            animator.Play("Helicopter_Left");
        }
        transform.localScale = helicopterScale;
        if (isAlive)
        {
            timer--;

            if (timer <= 0)
            {
                timer = timerMax;
                if (currentPoint == 0)
                {
                    StartCoroutine("PeriodicalShootRight");
                }
                else if (currentPoint == 1)
                {
                    StartCoroutine("PeriodicalShootLeft");
                }
            }
        }
    }
    IEnumerator PeriodicalShootRight()
    {
        for (shootAmount = 5f; shootAmount > 0f; shootAmount--)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += 3.5f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
            spawnPosition.y -= 1.2f;

            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
            newBullet.transform.position += transform.right;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, -25);
            newBullet.GetComponent<SpriteRenderer>().flipX = true;
            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(newBullet, 5);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator PeriodicalShootLeft()
    {
        for (shootAmount = 5f; shootAmount > 0f; shootAmount--)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x -= 3.5f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
            spawnPosition.y -= 1.2f;

            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
            newBullet.transform.position += -transform.right;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, 25);
            newBullet.GetComponent<SpriteRenderer>().flipX = false;
            newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(newBullet, 5);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
        //else if (!isAlive)
        //{
        //    speed = 0;
        //    transform.gameObject.tag = "Ground";
        //}
    

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && isAlive)
    //    {
    //        collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.up * impulse, ForceMode2D.Impulse);
    //    }
    //    else if (collision.gameObject.tag == "Molotov")
    //    {
    //        isAlive = false;
    //    }
    //}

