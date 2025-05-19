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
    public float totalTime = 10f; // Общее время для достижения финиша
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public MinigameState minigameState;

    [Header("Movement Settings")]
    public Transform movingObject; // Объект (медведь), который будет двигаться
    public Transform startPoint; // Стартовая точка
    public Transform finishPoint; // Финишная точка
    public float movementDuration = 10f; // Время за которое должен дойти до финиша

    private float currentProgress = 0f;
    private bool isGameOver = false;
    private bool isBearStopped = false;
    private float bearMovementTimer = 0f;

    private void Start()
    {
        resultPanel.SetActive(false);

        // Устанавливаем начальную позицию
        if (movingObject != null && startPoint != null)
        {
            movingObject.position = startPoint.position;
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
            // Обновляем таймер движения медведя
            bearMovementTimer += Time.deltaTime;
            currentProgress = bearMovementTimer / movementDuration;

            // Двигаем медведя от старта к финишу
            if (movingObject != null && startPoint != null && finishPoint != null)
            {
                movingObject.position = Vector3.Lerp(
                    startPoint.position,
                    finishPoint.position,
                    currentProgress
                );

                // Проверяем, достиг ли медведь финиша
                if (currentProgress >= 1f)
                {
                    GameOver(false);
                    return;
                }
            }
        }

        // Проверка заполнения слайдера
        if (shakeSlider.value >= 1f && !isBearStopped)
        {
            isBearStopped = true;
            GameOver(true);
            return;
        }

        // Постепенное уменьшение слайдера
        if (shakeSlider.value > 0f)
        {
            shakeSlider.value -= shakeDecreaseSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isGameOver || isBearStopped) return;

        // Проверка встряхивания устройства
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
            //resultText.color = Color.green;
            resultText.text = "Вы отпугнули медведя! Победа!";
            if (minigameState != null && !(minigameState.GetGameStatus() == 1))
            {
                minigameState.WinGame();
            }
        }
        else
        {
            //resultText.color = Color.yellow;
            resultText.text = "Вас съел медведь... Поражение!";
            if (minigameState != null && !(minigameState.GetGameStatus() == -1))
            {
                minigameState.LoseGame();
            }
        }
    }
}