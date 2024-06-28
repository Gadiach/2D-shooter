using UnityEngine;

public class ContinuousSound : Sounds
{
    
    public Transform playerTransform;  // ��������� �� �������� �����
    public float maxDistance = 10f;    


    void Update()
    {
        if (audioSource != null && playerTransform != null)
        {
            float distance = Vector2.Distance(playerTransform.position, transform.position);
            float maxVolume = 0.5f; // New maximum volume level
            float volume = Mathf.Clamp01(1f - distance / maxDistance) * maxVolume;

            // ������������ ������ �������� �������
            audioSource.volume = volume;
        }

        PlaySound(sounds[0]);
    } 
}
