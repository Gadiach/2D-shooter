using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZRKRocket_script : MonoBehaviour
{
    [SerializeField] private GameObject PS_RocketExplosion;
    public bool isDestroyed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        isDestroyed = true;           
        Instantiate(PS_RocketExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
}
