using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Add variables to store audio clips, volume levels, etc.

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This will ensure that the AudioManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager instances
        }
    }
    public static void PlaySound(AudioClip clip, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
    // Add methods to play, stop, and manage audio clips
}
