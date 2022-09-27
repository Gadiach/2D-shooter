using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
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

    public BackgroundScroller scroll;

    public float bulletTime;



    // public int coins;
    //public Text coinsText;

    public float healthMax;
    public float health;
    public Image healthImage;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public GameObject molotovPrefab;
    public float molotovSpeed;
    public float molotovTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        scroll.GetComponent<BackgroundScroller>();
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
            var isGrounded = IsGrounded();
            Debug.Log("1" + isGrounded);

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
            var isGrounded = IsGrounded();
            Debug.Log("2" + isGrounded);

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
        if (Input.GetKey(KeyCode.A))
        {
            scroll.scrollSpeed = -0.5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            scroll.scrollSpeed = 0.5f;
        }
        else
        {
            scroll.scrollSpeed = 0;
        }

        var isGrounded = IsGrounded();
        Debug.Log("3" + isGrounded);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (yPositionLastFrame > transform.position.y)
        {
            rb2d.AddForce(-transform.up * bonusGravity, ForceMode2D.Impulse);
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    animator.Play("Player_Jump");
        //}

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

            Destroy(newBullet, bulletTime);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject newMolo = Instantiate(molotovPrefab, transform.position, Quaternion.identity, null);

            if (spriteRenderer.flipX == true && Input.GetKeyDown(KeyCode.C))
            {
                //animator.Play("Shoot_Left");
                newMolo.transform.position += -transform.right;
                newMolo.GetComponent<Rigidbody2D>().AddForce(-transform.right * molotovSpeed, ForceMode2D.Impulse);
            }
            else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.C))
            {
                //animator.Play("Shoot_Right");
                newMolo.transform.position += transform.right;
                newMolo.GetComponent<Rigidbody2D>().AddForce(transform.right * molotovSpeed, ForceMode2D.Impulse);
            }

            Destroy(newMolo, molotovTime);
        }

    }

    private void LateUpdate()
    {
        yPositionLastFrame = transform.position.y;
        
        health = Mathf.Clamp(health, 0, healthMax);

        healthImage.fillAmount = health / healthMax;


        yPositionLastFrame = transform.position.y;

        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = false;
    //    }
    //}
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayerMask);
        Debug.Log("123" + groundCheck.collider );
        Debug.DrawRay(transform.position, Vector2.down * 1f);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");

    }
}
