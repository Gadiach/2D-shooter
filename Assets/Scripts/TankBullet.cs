using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private SpriteRenderer bulletSpriteRenderer;

    public float damage = 0.25f;
    public float impulse = 0.01f;

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.up * impulse, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "TankBullet" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "EnemySoldier" || collision.gameObject.tag == "HelicopterBulletPrefab")
        {            
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
