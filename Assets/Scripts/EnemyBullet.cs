using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private SpriteRenderer bulletSpriteRenderer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
