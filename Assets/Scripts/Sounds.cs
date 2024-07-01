using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    public AudioSource audioSource => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.86f, float p2 = 0.99f)
    {
        audioSource.pitch = Random.Range(p1, p2);
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayFootstepSound(AudioClip clip) // for one foot step , it is for events
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void LoopSound()
    {
        // Check if the AudioSource is still playing (it might have been stopped manually)
        if (!audioSource.isPlaying)
        {
            // Restart the sound
            PlaySound(sounds[0]);
        }
    }

    public void PlayLoopingSound(AudioClip clip)
    {
        // Play the sound
        audioSource.Play();

        // Invoke the LoopSound method after the duration of the audio clip
        Invoke("LoopSound", clip.length);
    }

    public void PlaySoundAudioSrc2(AudioSource audioSource2)
    {
        audioSource2.Play();
    }
}
