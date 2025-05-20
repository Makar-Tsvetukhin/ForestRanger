using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShakeGame : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeThreshold = 2.5f;
    public float shakeFillSpeed = 0.2f;
    public float shakeDecreaseSpeed = 0.1f;
    public Slider shakeSlider;

    [Header("Game Settings")]
    public float totalTime = 10f;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public MinigameState minigameState;
    public AudioSource bearSound;

    [Header("Movement Settings")]
    public Transform movingObject;
    public Transform startPoint;
    public Transform finishPoint;
    public float movementDuration = 10f;
    public Animator bearAnimator;

    private float currentProgress = 0f;
    private bool isGameOver = false;
    private bool isBearStopped = false;
    private float bearMovementTimer = 0f;

    private void Start()
    {
        resultPanel.SetActive(false);

        if (movingObject != null && startPoint != null)
        {
            movingObject.position = startPoint.position;
        }

        if (bearSound != null)
        {
            bearSound.Play();
        }

#if !UNITY_EDITOR
        shakeSlider.interactable = false;
#endif
    }

    private void Update()
    {
        if (isGameOver) return;

        if (!isBearStopped)
        {
            bearMovementTimer += Time.deltaTime;
            currentProgress = bearMovementTimer / movementDuration;

            if (movingObject != null && startPoint != null && finishPoint != null)
            {
                movingObject.position = Vector3.Lerp(
                    startPoint.position,
                    finishPoint.position,
                    currentProgress
                );

                if (currentProgress >= 1f)
                {
                    if (bearSound != null)
                    {
                        bearSound.Stop();
                    }
                    if (bearAnimator != null)
                    {
                        bearAnimator.enabled = false;
                    }
                    GameOver(false);
                    return;
                }
            }
        }

        if (shakeSlider.value >= 1f && !isBearStopped)
        {
            if (bearSound != null)
            {
                bearSound.Stop();
            }
            if (bearAnimator != null)
            {
                bearAnimator.enabled = false;
            }
            isBearStopped = true;
            GameOver(true);
            return;
        }

        if (shakeSlider.value > 0f)
        {
            shakeSlider.value -= shakeDecreaseSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isGameOver || isBearStopped) return;

        Vector3 acceleration = Input.acceleration;
        float shakeForce = acceleration.magnitude;

        if (shakeForce > shakeThreshold)
        {
            shakeSlider.value += shakeForce * shakeFillSpeed * Time.deltaTime;
        }
    }

    private void GameOver(bool isWin)
    {
        isGameOver = true;
        resultPanel.SetActive(true);

        if (isWin)
        {
            resultText.text = "Вы отпугнули медведя! Победа!";
            if (minigameState != null && !(minigameState.GetGameStatus() == 1))
            {
                minigameState.WinGame();
            }
        }
        else
        {
            resultText.text = "Вас съел медведь... Поражение!";
            if (minigameState != null && !(minigameState.GetGameStatus() == -1))
            {
                minigameState.LoseGame();
            }
        }
    }
}