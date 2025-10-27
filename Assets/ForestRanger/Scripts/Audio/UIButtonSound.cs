using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip customClip;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (AudioManager.Instance == null)
            return;

        if (customClip != null)
            AudioManager.Instance.PlaySound(customClip);
        else
            AudioManager.Instance.PlayClick();
    }
}
