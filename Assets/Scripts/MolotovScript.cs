using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovScript : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;

    public float damage = 50f;
    public float impulse;

    private float timer;
    public float timerMax = 80;

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
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Tank")
        {
            Instantiate(firePrefab, transform.position,firePrefab.transform.rotation);            
            Destroy(gameObject);           
        }
    }
}
