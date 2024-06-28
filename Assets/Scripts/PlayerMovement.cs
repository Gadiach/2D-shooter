using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : Sounds
{
    [SerializeField] private LayerMask platformLayerMask;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public bool isCrawled;
    public float impulseSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    
    private Animator animator;

    private float yPositionLastFrame;
    public float bonusGravity;

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
        Pzrk,
        Molotov
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

                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) )
                    {
                        isCrawled = true;
                        animator.Play("Player_Crawl");                        
                    }
                    else
                    {                        
                        animator.Play("Player_Run");
                        isCrawled = false;                        
                    }
                }
                
                if(!isGrounded)
                {                   
                    animator.Play("Player_Jump");                    
                }               
            }
            else
            {
                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        isCrawled = true;
                        animator.Play("Player_Crawl_Idle");
                    }
                    else
                    {
                        isCrawled = false;
                        animator.Play("Player_Idle");
                    }
                }
                else if (!isGrounded)
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

                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                    {
                        isCrawled = true;
                        animator.Play("Player_PZRK_crawl");
                    }                   
                    else
                    {
                        animator.Play("Player_PZRK_run");
                        isCrawled = false;
                    }
                }

                if (!isGrounded)
                {
                    animator.Play("Player_PZRK_jump");
                }
            }
            else
            {
                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        isCrawled = true;
                        animator.Play("Player_PZRK_crawl_idle");
                    }
                    else
                    {
                        isCrawled = false;
                        animator.Play("Player_PZRK_Idle");
                    }
                }
                else if (!isGrounded)
                {
                    animator.Play("Player_PZRK_jump");
                }
            }
        }
        else if (fightRegime == FightRegime.Molotov)
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

                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                    {
                        isCrawled = true;
                        animator.Play("Player_Molotov_crawl");
                    }
                    else
                    {
                        animator.Play("Player_Molotov_Run");
                        isCrawled = false;
                    }
                }

                if (!isGrounded)
                {
                    animator.Play("Player_Molotov_Jump");
                }
            }
            else
            {
                isGrounded = IsGrounded();

                if (isGrounded)
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        isCrawled = true;
                        animator.Play("Player_Molotov_crawl_idle");
                    }
                    else
                    {
                        isCrawled = false;
                        animator.Play("Player_Molotov_Idle");
                    }
                }
                else if (!isGrounded)
                {
                    animator.Play("Player_Molotov_Jump");
                }
            }
        }
        transform.position = currentPosition;
    }

    private void Update()
    {

        RegimeChange();

        BackgroundScroll();
    
        isGrounded = IsGrounded();

        // SOUNDS

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            PlaySound(sounds[0]);
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isCrawled)
            {
                StandShoot();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && isCrawled)
            {
                CrawledShoot();               
            }           
        }
        else if (fightRegime == FightRegime.Pzrk)
        {           
            if (Input.GetKeyDown(KeyCode.Mouse0) && isLoaded && !isCrawled && isGrounded)
            {
                PlaySound(sounds[6]);

                PZRKShoot();
            }
        }
        else if (fightRegime == FightRegime.Molotov)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                PlaySound(sounds[5]);
                //animator.Play("Player_Molotov_throw");
                GameObject newMolo = Instantiate(molotovPrefab, transform.position, Quaternion.identity, null);

                if (spriteRenderer.flipX == true && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Shoot_Left
                    newMolo.transform.position += -transform.right;
                    newMolo.GetComponent<Rigidbody2D>().AddForce(-transform.right * molotovSpeed, ForceMode2D.Impulse);
                }
                else if (spriteRenderer.flipX == false && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Shoot_Right
                    newMolo.transform.position += transform.right;
                    newMolo.GetComponent<Rigidbody2D>().AddForce(transform.right * molotovSpeed, ForceMode2D.Impulse);
                }
                fightRegime = FightRegime.Gun;
                Destroy(newMolo, molotovTime);
            }
        }
    }

    private void LateUpdate()
    {
        UpdateHealthStatus();
    }

    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, platformLayerMask);
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.green);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    } 
    
    private void RegimeChange()
    {
        if (fightRegime == FightRegime.Gun && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Pzrk;
        }
        else if (fightRegime == FightRegime.Pzrk && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Gun;
        }
        else if (fightRegime == FightRegime.Molotov && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Pzrk;
        }
        else if (fightRegime == FightRegime.Molotov && Input.GetKeyDown(KeyCode.C))
        {
            fightRegime = FightRegime.Gun;
        }
        else if (fightRegime == FightRegime.Molotov && Input.GetKeyDown(KeyCode.F))
        {
            fightRegime = FightRegime.Pzrk;
        }
        else if (fightRegime == FightRegime.Molotov && Input.GetKeyDown(KeyCode.C))
        {
            fightRegime = FightRegime.Gun;
        }
        else if (fightRegime == FightRegime.Gun && Input.GetKeyDown(KeyCode.C))
        {
            fightRegime = FightRegime.Molotov;
        }
        else if (fightRegime == FightRegime.Pzrk && Input.GetKeyDown(KeyCode.C))
        {
            fightRegime = FightRegime.Molotov;
        }
    }

    private void BackgroundScroll()
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
    }

    private void StandShoot()
    {
        PlaySound(sounds[4]);
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

    private void CrawledShoot()
    {
        PlaySound(sounds[4]);
        Vector3 spawnPosition = transform.position;

        spawnPosition.y = -1.45f; //якщо куля створюється сильно далеко від player можна зробити тут меньше значення

        GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);

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

    private void PZRKShoot()
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

    private void UpdateHealthStatus()
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
}
