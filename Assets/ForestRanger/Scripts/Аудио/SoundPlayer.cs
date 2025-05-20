using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; 
    private bool isSoundPlaying = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No AudioSource found on this object!");
            }
        }
    }

    public void PlaySound()
    {
        if (!isSoundPlaying && audioSource != null && audioSource.clip != null)
        {
            StartCoroutine(PlaySoundOnce());
        }
    }

    private System.Collections.IEnumerator PlaySoundOnce()
    {
        isSoundPlaying = true;
        audioSource.Play(); 

        yield return new WaitForSeconds(audioSource.clip.length);

        isSoundPlaying = false;
    }
}