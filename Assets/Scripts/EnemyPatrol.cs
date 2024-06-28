using UnityEngine;

public class EnemyPatrol : Sounds
{
    public Transform playerTransform;  // Посилання на головний герой
    public float maxDistance = 10f;    

    public Vector2[] points;
    public int currentPoint;

    public float speed;

    private SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;    
    public float bulletSpeed;

    private float timer;
    public float timerMax = 80;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (audioSource != null && playerTransform != null)
        {
            float distance = Vector2.Distance(playerTransform.position, transform.position);
            float volume = Mathf.Clamp01(1f - distance / maxDistance);           

            // Встановлення нового значення гучності
            audioSource.volume = volume;
        }      

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
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint], speed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        timer--;

        if (timer <= 0)
        {
            timer = timerMax;
            if (currentPoint == 0)
            {
                PlaySound(sounds[0]);
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
                newBullet.transform.position += -transform.right;                
                newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 1);
            }
            else if (currentPoint == 1)
            {
                PlaySound(sounds[0]);
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
                newBullet.transform.position += transform.right;                
                newBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(newBullet, 1);
            }
        }
    }
}
