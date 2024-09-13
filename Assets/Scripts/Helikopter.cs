using System.Collections;
using UnityEngine;

public class Helikopter : Sounds
{
    public Transform playerTransform;  // Посилання на головний герой
    public float maxDistance = 10f;   

    public Vector2[] points;
    public int currentPoint;


    public float speed;
    public float fallingSpeed;

    public float health = 100f;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb2d;

    public GameObject bulletPrefab;
    private GameObject newBullet;
    public float bulletSpeed;

    private float timer;
    public float timerMax = 80;

    public float damage = 50f;
    public float impulse = 1f;

    private Animator animator;

    public bool isAlive = true;

    public float shootAmount;

    public float crushSpeed;
    private bool isCrushed = false;

    [SerializeField] private PolygonCollider2D collider2D;

    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private GameObject explosionEffect;
    private ParticleSystem.ShapeModule shootEffectShape;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>() ;        
        shootEffectShape = shootEffect.shape;
    }

    private void Update()
    {
        audioSource1.volume = 0.1f;
        audioSource2.volume = 0.2f;
        Vector2 position = transform.position;

        if (isAlive)
        {
            if (audioSource != null && playerTransform != null)
            {
                float distance = Vector2.Distance(playerTransform.position, transform.position);
                float maxVolume = 0.5f; // New maximum volume level
                float volume = Mathf.Clamp01(1f - distance / maxDistance) * maxVolume;

                audioSource.volume = volume;
            }

            if (position == points[currentPoint])
            {
                if (currentPoint == 0)
                {
                    currentPoint = 1;
                    shootEffectShape.rotation = new Vector3(0f, 0f, 0f);                   
                }
                else if (currentPoint == 1)
                {
                    currentPoint = 0;
                    shootEffectShape.rotation = new Vector3(0f, 0f, 177f);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, points[currentPoint], speed * Time.deltaTime);
        }
        else if (!isAlive)
        {
            
            rb2d.AddForce(-transform.up * crushSpeed, ForceMode2D.Force);
            if (isCrushed)
            {
                Vector3 temp = transform.position;
                transform.gameObject.tag = "Ground";                
                temp.y = -1.2f;
                transform.position= temp;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 helicopterScale = transform.localScale;
        if (!isCrushed)
        {
            if (currentPoint == 0)
            {
                animator.Play("Helicopter_Right");
                if (isCrushed)
                {
                    animator.Play("Helicopter_Destroyed");
                }
            }
            else if (currentPoint == 1)
            {
                animator.Play("Helicopter_Left");
            }
        }
        else if (isCrushed)
        {
            smokeEffect.Stop();
            if (currentPoint == 0)
            {
                animator.Play("Helicopter_Destroyed");
            }
            else if (currentPoint == 1)
            {
                animator.Play("Helicopter_Destroyed");
            }
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
                    PlaySoundAudioSrc2(audioSource2);
                    StartCoroutine("PeriodicalShootRight");
                }
                else if (currentPoint == 1)
                {
                    PlaySoundAudioSrc2(audioSource2);
                    StartCoroutine("PeriodicalShootLeft");
                }
            }
        }
        else
        {
            rb2d.mass = 1.0f;
        }
        
    }
    IEnumerator PeriodicalShootRight()
    {
        for (shootAmount = 5f; shootAmount > 0f; shootAmount--)
        {
            shootEffect.Play();
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += 3.8f; 
            spawnPosition.y -= 1.4f;

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
            shootEffect.Play();
            Vector3 spawnPosition = transform.position;
            spawnPosition.x -= 3.8f; //якщо куля створюється сильно далеко від танка можна зробити тут меньше значення
            spawnPosition.y -= 1.4f;

            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
            newBullet.transform.position += -transform.right;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, 25);
            newBullet.GetComponent<SpriteRenderer>().flipX = false;
            newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(newBullet, 5);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pzrk")
        {
            isAlive = false;
            smokeEffect.Play();
            Destroy(collision.gameObject);          
        }
        if (collision.gameObject.tag == "Ground")
        {
            collider2D.isTrigger = true;
            explosionEffect.SetActive(true);
            smokeEffect.Stop();
            isCrushed = true;             
        }
    }    
}
//else if (!isAlive)
//{
//    speed = 0;
//    transform.gameObject.tag = "Ground";
//}




