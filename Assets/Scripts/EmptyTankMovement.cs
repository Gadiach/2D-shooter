using UnityEngine;

public class EmptyTankMovement : Sounds
{
    [SerializeField] private GameObject particleSysObj;
    [SerializeField] private ParticleSystem shootEffect;
    private ParticleSystem.ShapeModule shape;
    public Transform playerTransform;  // Посилання на головний герой
    public float maxDistance = 10f;    

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

    private Rigidbody2D rb2d;

    [SerializeField] private float engineVolume;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        shape = shootEffect.shape;
    }

    private void Update()
    {
        RegulateTankEngineSound();
        RegulateTankShootSound();

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
        
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint], speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
        
        if (isAlive)
        {
            Vector3 tankScale = transform.localScale;

            if (currentPoint == 0)
            {
                animator.Play("Tank_Right");
                tankScale.x = -0.5f;
                particleSysObj.transform.position = new Vector3(transform.position.x - 3.5f, -1.1f, -0.3f);
            }
            else if (currentPoint == 1)
            {
                animator.Play("Tank");
                tankScale.x = 0.5f;
                particleSysObj.transform.position = new Vector3(transform.position.x + 3.5f, -1.1f, -0.3f);
            }
            transform.localScale = tankScale;

            timer--;

            if (timer <= 0)
            {
                timer = timerMax;
                if (currentPoint == 0)
                {
                    Vector3 spawnPosition = transform.position;
                    spawnPosition.x += 4.5f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
                    spawnPosition.y += 0.5f;

                    //PlaySound(sounds[0]);
                    audioSource2.Play();
                    Debug.Log("shot");
                    shootEffect.transform.position = new Vector3(transform.position.x + 5f, 0.02f, -0.3f);
                    //shootEffect.transform.rotation = new Quaternion(-270f, 180f, -0.10f, 0);
                    shape.rotation = new Vector3(0f, 0f, 0f);
                    shootEffect.Play();
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

                    //PlaySound(sounds[0]);
                    audioSource2.Play();
                    shootEffect.transform.position = new Vector3(transform.position.x - 5f, 0.02f, -0.3f);
                    //shootEffect.transform.rotation = new Quaternion(-90f, 180f, -0.10f, 0);
                    shape.rotation = new Vector3(0f, 180f, 0f);
                    shootEffect.Play();
                    Debug.Log("shot2");
                    GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                    newBullet.transform.position += -transform.right;
                    newBullet.GetComponent<SpriteRenderer>().flipX = false;
                    newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
                    Destroy(newBullet, 5);
                }
            }
        }
        else if (!isAlive)
        {
            speed = 0;
            transform.gameObject.tag = "Ground";
            animator.Play("Tank_Idle");
            particleSysObj.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // PlaySound(burning);
        if (collision.gameObject.tag == "Player" && isAlive)
        {
            collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.up * impulse, ForceMode2D.Impulse);            
        }
        else if (collision.gameObject.tag == "Molotov")
        {
            isAlive = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void RegulateTankEngineSound()
    {
        if (audioSource != null && playerTransform != null)
        {
            float distance = Vector2.Distance(playerTransform.position, transform.position);

            // Розрахунок гучності в залежності від відстані
            float volume = Mathf.Clamp01(0.4f - distance / maxDistance) * engineVolume;

            // Встановлення нового значення гучності
            audioSource.volume = volume;
        }
    }

    private void RegulateTankShootSound()
    {
        if (audioSource != null && playerTransform != null)
        {
            float distance = Vector2.Distance(playerTransform.position, transform.position);

            // Розрахунок гучності в залежності від відстані
            float volume = Mathf.Clamp01(0.6f - distance / maxDistance);

            // Встановлення нового значення гучності
            audioSource2.volume = volume;
        }
    }
}
