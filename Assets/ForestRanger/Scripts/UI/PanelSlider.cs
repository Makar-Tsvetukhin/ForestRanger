using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PanelSlider : MonoBehaviour
{
	[field: SerializeField] private RectTransform panel;
	[field: SerializeField] private RectTransform button;
	[field: SerializeField] private RectTransform targetPosition;
	[field: SerializeField] private float animationTime = 0.3f;

    private Vector2 startPanelPosition;
    private Coroutine animationRoutine;
    private bool isHidden;

	private void Start()
    {
		startPanelPosition = panel.anchoredPosition;
		TogglePanel();
	}

    public void TogglePanel()
    {
        if (animationRoutine != null) StopCoroutine(animationRoutine);
        animationRoutine = StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
		Vector2 startPos = panel.anchoredPosition;
		Vector2 targetPos = isHidden ? startPanelPosition : targetPosition.anchoredPosition;
		float startRot = button.localEulerAngles.z;
		float targetRot = isHidden ? 0 : 180;
		float elapsed = 0;

        while (elapsed < animationTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationTime;
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            button.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(startRot, targetRot, t));
            yield return null;
        }

        panel.anchoredPosition = targetPos;
        button.localEulerAngles = new Vector3(0, 0, targetRot);
        isHidden = !isHidden;
    }
}