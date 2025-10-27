using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float soundDuration = 3f;
    [SerializeField] private float fadeOutTime = 0.5f;

    private bool isSoundPlaying = false;
    private float originalVolume;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No AudioSource found on this object!");
                return;
            }
        }

        originalVolume = audioSource.volume;
    }

    public void PlaySound()
    {
        if (!isSoundPlaying && audioSource != null && audioSource.clip != null)
        {
            StartCoroutine(PlaySoundWithFade());
        }
    }

    private System.Collections.IEnumerator PlaySoundWithFade()
    {
        isSoundPlaying = true;
        audioSource.volume = originalVolume;
        audioSource.Play();
        yield return new WaitForSeconds(soundDuration - fadeOutTime);
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = originalVolume;
        isSoundPlaying = false;
    }
}
