using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySoundOnSceneLoad : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName && audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
    