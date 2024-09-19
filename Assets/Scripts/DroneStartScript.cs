using UnityEngine;
using System.Collections;

public class DroneStartScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement player; 
    [SerializeField] private float speed = 4f; 
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private float duration = 2f; 

    private Coroutine movementCoroutine; 

    void Update()
    {     
        if (player.fightRegime == PlayerMovement.FightRegime.FPV)
        {           
            movementCoroutine = StartCoroutine(MoveAndPlaySound());            
        }
        else
        {           
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }           
        }
    }

    private IEnumerator MoveAndPlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += speed * Time.deltaTime;
            transform.position = newPosition;
            yield return null; 
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Destroy(gameObject);

        GameEvents.SwitchToDrone();
    }
}