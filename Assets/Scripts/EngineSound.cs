using UnityEngine;

public class EngineSound : ContinuousSound
{
    public GameObject helicopterObject;
    private Helikopter helicopter;

    void Start()
    {
        // �������� ��������� Helikopter �� ������� helicopterObject
        helicopter = helicopterObject.GetComponent<Helikopter>();        
    }

    void Update()
    {
        Debug.Log("Works");
        if (helicopter.isAlive)
        {
            Debug.Log("IsAlive");
            if (audioSource != null && playerTransform != null)
            {
                float distance = Vector2.Distance(playerTransform.position, transform.position);
                float maxVolume = 0.5f; // New maximum volume level
                float volume = Mathf.Clamp01(1f - distance / maxDistance) * maxVolume;

                // ������������ ������ �������� �������
                audioSource.volume = volume;
            }

            PlayLoopingSound(sounds[0]);
        }
        else
        {
            audioSource.volume = 0f;
            Debug.Log("IsNotAlive");
        }
    }  
}
