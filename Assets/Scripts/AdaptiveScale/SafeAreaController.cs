using UnityEngine;

public class SafeAreaHandler : MonoBehaviour
{
    [Header("UI, который нужно сдвинуть внутрь Safe Area")]
    public RectTransform uiRoot;

    private Rect _lastSafeArea;

    void Start()
    {
        ApplySafeArea();
    }

    void Update()
    {
        if (_lastSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        _lastSafeArea = safeArea;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (uiRoot)
        {
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= screenWidth;
            anchorMin.y /= screenHeight;
            anchorMax.x /= screenWidth;
            anchorMax.y /= screenHeight;

            uiRoot.anchorMin = anchorMin;
            uiRoot.anchorMax = anchorMax;
            uiRoot.offsetMin = Vector2.zero;
            uiRoot.offsetMax = Vector2.zero;
        }
    }
}
