using UnityEngine;

public class PS_destroyAfterTime : MonoBehaviour
{
    private float lifetime = 7f;


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
