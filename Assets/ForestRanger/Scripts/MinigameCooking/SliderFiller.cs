using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    public Slider slider;
    public Button button;
    private float fillDuration = 5f;
    private float resetDelay = 0.5f;
    private bool isFilling = false;
    private bool isResetting = false;
    private float currentTime = 0f;

    void Start()
    {
        button.onClick.AddListener(StartFilling);
        slider.value = 0f;
    }

    void Update()
    {
        if (isFilling)
        {
            currentTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(currentTime / fillDuration);

            if (currentTime >= fillDuration)
            {
                isFilling = false;
                isResetting = true;
                currentTime = 0f;
            }
        }

        if (isResetting)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= resetDelay)
            {
                slider.value = 0f;
                isResetting = false;
                currentTime = 0f;
            }
        }
    }

    void StartFilling()
    {
        if (!isFilling && !isResetting)
        {
            slider.value = 0f;
            isFilling = true;
            currentTime = 0f;
        }
    }
}