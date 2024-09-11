using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    [SerializeField] private float lifetime = 3.0f;  

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
