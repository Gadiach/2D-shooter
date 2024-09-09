using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovScript : MonoBehaviour
{
    [SerializeField] private GameObject longFirePrefab;
    [SerializeField] private GameObject shortFirePrefab;
    [SerializeField] private Collider2D collider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public float damage = 50f;
    public float impulse;

    private float timer;
    public float timerMax = 80;
    public bool isDestroyed = false;

    private void FixedUpdate()
    {
        timer--;

        if (timer <= 0)
        {
            timer = timerMax;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isDestroyed)
        {
            isDestroyed = true;
            //Instantiate(longFirePrefab, transform.position, Quaternion.Euler(-90, -90, 90));
            Instantiate(longFirePrefab, transform.position, Quaternion.Euler(-90, -90, 90));
            spriteRenderer.enabled = false;
            //collider.enabled = false;
            //Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Tank" && !isDestroyed)
        {
            isDestroyed = true;
            //Instantiate(shortFirePrefab, transform.position, Quaternion.Euler(-90, -90, 90));
            var fire = Instantiate(longFirePrefab, transform.position, Quaternion.Euler(-90, -90, 90));
            fire.transform.SetParent(collision.transform);
            spriteRenderer.enabled = false;
            //collider.enabled = false;
            //Destroy(gameObject);
        }    
    }   
}
