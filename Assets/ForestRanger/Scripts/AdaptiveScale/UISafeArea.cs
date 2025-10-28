using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UISafeAreaOverlay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        ApplyAdjustedSafeArea();
    }

    private void ApplyAdjustedSafeArea()
    {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;

        if (background == null) return;

        float spriteAspect = background.sprite.bounds.size.x / background.sprite.bounds.size.y;
        float screenAspect = (float)Screen.width / Screen.height;

        if (screenAspect > spriteAspect)
        {
            float visiblePercent = spriteAspect / screenAspect;
            float sideOffset = (1f - visiblePercent) / 2f;

            rect.anchorMin = new Vector2(sideOffset, rect.anchorMin.y);
            rect.anchorMax = new Vector2(1f - sideOffset, rect.anchorMax.y);
        }
    }
}
