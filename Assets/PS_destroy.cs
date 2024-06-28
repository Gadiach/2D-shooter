using UnityEngine;

public class PS_destroy : MonoBehaviour
{
    private float lifetime = 7f; 


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
