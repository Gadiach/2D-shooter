using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pzrk : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Helicopter")
        {
            Destroy(gameObject);
            Debug.Log("1");
            
        }        
    }

} 



