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
  
    public float healthMax;
    public float health;
    public Image healthImage;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public GameObject molotovPrefab;
    public float molotovSpeed;
    public float molotovTime;

    public GameObject PzrkRocketPrefub;
     public enum FightRegime
    {
        Gun,
        Pzrk
    }

    public FightRegime fightRegime;
   
    public float rocketSpeed = 10f;

    private Vector3 rocketRight = new Vector3(2, 1, 0);
    private Vector3 rocketLeft = new Vector3(-2, 1, 0);

    public bool isLoaded = true;
    public float loadMax;
    public float load = 3000f;
    public Image loadImage;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scroll.GetComponent<BackgroundScroller>();                
    }
    private void FixedUpdate()
    {       
        Vector3 currentPosition = transform.position;
        if (fightRegime == FightRegime.Gun)
        {
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

                if (isGrounded)
                {
                    animator.Play("Player_Idle");
                }
                else
                {
                    animator.Play("Player_Jump");
                }
            }
            
        }
        else if (fightRegime == FightRegime.Pzrk)
        {
            
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
                animator.Play("Player_PZRK");
            }
            else 
            animator.Play("Player_PZRK_Idle");
        }
        transform.position = currentPosition;
    }

    private void Update()
    {
        if (fightRegime == FightRegime.Gun && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Pzrk;           
        }
        else if (fightRegime == FightRegime.Pzrk && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Gun;
        }

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

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (yPositionLastFrame > transform.position.y)
        {
            rb2d.AddForce(-transform.up * bonusGravity, ForceMode2D.Impulse);
        }
        
        if (transform.position.y <= -3.6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (fightRegime == FightRegime.Gun)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);

                if (spriteRenderer.flipX == true && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Shoot_Left
                    newBullet.transform.position += -transform.right;
                    newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
                }
                else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Shoot_Right
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
                    //Shoot_Left
                    newMolo.transform.position += -transform.right;
                    newMolo.GetComponent<Rigidbody2D>().AddForce(-transform.right * molotovSpeed, ForceMode2D.Impulse);
                }
                else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.C))
                {
                    //Shoot_Right
                    newMolo.transform.position += transform.right;
                    newMolo.GetComponent<Rigidbody2D>().AddForce(transform.right * molotovSpeed, ForceMode2D.Impulse);
                }

                Destroy(newMolo, molotovTime);
            }
        }
        else if (fightRegime == FightRegime.Pzrk)
        {           
            if (Input.GetKeyDown(KeyCode.Mouse0) && isLoaded)
            {
                Vector3 spawnPosition = transform.position;
                
                if (spriteRenderer.flipX == true && Input.GetKeyDown(KeyCode.Mouse0)) //shoot left
                {                    
                    spawnPosition.x -= 0.01f; 
                    spawnPosition.y += 0.9f;
                    GameObject newBullet = Instantiate(PzrkRocketPrefub, spawnPosition, Quaternion.identity, null);
                    newBullet.transform.position += -transform.right;
                    newBullet.GetComponent<SpriteRenderer>().flipX = true;
                    newBullet.transform.rotation = Quaternion.Euler(0, 0, -26);
                    newBullet.GetComponent<Rigidbody2D>().AddForce(rocketLeft * bulletSpeed, ForceMode2D.Impulse);
                    Destroy(newBullet, bulletTime);
                }
                else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.Mouse0)) //shoot right
                {
                    spawnPosition.x += 0.01f;
                    spawnPosition.y += 0.9f;
                    GameObject newBullet = Instantiate(PzrkRocketPrefub, spawnPosition, Quaternion.identity, null);
                    newBullet.transform.position += transform.right;
                    newBullet.GetComponent<SpriteRenderer>().flipX = false;
                    newBullet.transform.rotation = Quaternion.Euler(0, 0, 26);
                    newBullet.GetComponent<Rigidbody2D>().AddForce(rocketRight * bulletSpeed, ForceMode2D.Impulse);
                    Destroy(newBullet, bulletTime);
                }
                load = 0f;
            }
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
        load += 1f;
        load = Mathf.Clamp(load, 0, loadMax);
        if (load == loadMax)
        {
            isLoaded = true;
            loadImage.enabled = true;
        }
        else if (load < loadMax)
        {
            isLoaded = false;
            loadImage.enabled = false;
        }
    }
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, platformLayerMask);       
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    }
}
