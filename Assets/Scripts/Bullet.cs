using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    public float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemySoldier" || collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Putin")
        {
            collision.gameObject.GetComponent<Putin>().health -= damage;
            Destroy(gameObject);
        }
        else if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
