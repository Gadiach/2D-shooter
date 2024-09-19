using UnityEngine;

public class FPVTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private bool isFPVAlive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isFPVAlive)
        {
            playerScript.IsPlayerInFPVTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.IsPlayerInFPVTrigger = false;
        }
    }
}