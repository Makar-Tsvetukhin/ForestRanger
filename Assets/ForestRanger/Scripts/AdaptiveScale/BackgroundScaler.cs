using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleBackgroundByHeight();
    }

    private void ScaleBackgroundByHeight()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float spriteHeight = _spriteRenderer.sprite.bounds.size.y;

        float scale = cameraHeight / spriteHeight;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
