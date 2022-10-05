using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBulletPrefab : MonoBehaviour
{
    private SpriteRenderer bulletSpriteRenderer;

    public new Transform transform;

    public float damage = 50f;
    public float impulse;

    [SerializeField] private LayerMask platformLayerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().health -= damage;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.up * impulse, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "TankBullet" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}
