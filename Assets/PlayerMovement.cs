using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public float impulseSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    //private Animator animator;

    private float yPositionLastFrame;
    public float bonusGravity;

    // public int coins;
    //public Text coinsText;

    //public float healthMax;
    //public float health;
    //public Image healthImage;

    //public GameObject bulletPrefab;
    //public float bulletSpeed;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        //coinsText.text = coins.ToString();
    }
    private void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                currentPosition.x -= speed;
                spriteRenderer.flipX = true;
            }

            else if (Input.GetKey(KeyCode.D))
            {
                currentPosition.x += speed;
                spriteRenderer.flipX = false;
            }
        }
        transform.position = currentPosition;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (yPositionLastFrame > transform.position.y)
        {
            rb2d.AddForce(-transform.up * bonusGravity, ForceMode2D.Impulse);
        }
    }

    private void LateUpdate()
    {
        yPositionLastFrame = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
