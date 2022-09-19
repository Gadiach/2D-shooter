using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Vector2[] points;
    public int currentPoint;

    public float speed;

    private SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;
    private GameObject newBullet;
    public float bulletSpeed;

    private float timer;
    public float timerMax = 80;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        Vector2 position = transform.position;

        if (position == points[currentPoint])
        {
            if (currentPoint == 0)
            {
                currentPoint = 1;
                spriteRenderer.flipX = false;

            }
            else if (currentPoint == 1)
            {
                currentPoint = 0;
                spriteRenderer.flipX = true;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint], speed);

    }

    private void FixedUpdate()
    {
        timer--;

        if (timer <= 0)
        {
            timer = timerMax;
            if (currentPoint == 0)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
                newBullet.transform.position += -transform.right;                
                newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 1);
            }
            else if (currentPoint == 1)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
                newBullet.transform.position += transform.right;                
                newBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 1);
            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
