using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public float impulseSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    
    private Animator animator;

    private float yPositionLastFrame;
    public float bonusGravity;

    private Camera _cam;
    
    // public int coins;
    //public Text coinsText;

    //public float healthMax;
    //public float health;
    //public Image healthImage;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    /*private void Awake()
    {
        _cam = Camera.main;
    }*/

   
    
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

            if (isGrounded)
            {
                animator.Play("Player_Run");
            }
            else
            {
                animator.Play("Player_Jump");
            }
        }
        else
        {
            if (isGrounded)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Jump");
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("Player_Jump");
        }

        if (transform.position.y <= -3.6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }       

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);

            if (spriteRenderer.flipX == true && Input.GetKeyDown(KeyCode.Mouse0))
            {
                //animator.Play("Shoot_Left");
                newBullet.transform.position += -transform.right;
                newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
            }
            else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.Mouse0))
            {
                //animator.Play("Shoot_Right");
                newBullet.transform.position += transform.right;
                newBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
            }

            Destroy(newBullet, 1);
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
