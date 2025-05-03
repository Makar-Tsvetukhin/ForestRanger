using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShakeGame : MonoBehaviour
{
    public float shakeThreshold = 2.5f;
    public float shakeFillSpeed = 0.2f;
    public float shakeDecreaseSpeed = 0.1f;
    public Slider shakeSlider;
    public float timeLimit = 10f;
    public TextMeshProUGUI timerText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public MinigameState minigameState;

    private float currentTime;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();
        resultPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);

#if !UNITY_EDITOR
    shakeSlider.interactable = false;
#endif
    }

    private void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            GameOver(false);
            return;
        }

        if (shakeSlider.value >= 1f)
        {
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
        if (isGameOver) return;

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
            resultText.color = Color.green;
            resultText.text = "Вы отпугнули медведя! Победа!";
            if (!(minigameState.GetGameStatus() == 1))
            {
                minigameState.WinGame();
            }
        }
        else
        {
            resultText.color = Color.yellow;
            resultText.text = "Вас съел медведь... Поражение!";
			if (!(minigameState.GetGameStatus() == -1))
			{
				minigameState.LoseGame();
			}
		}
    }

    private void UpdateTimerUI()
    {
        timerText.text = $"{Mathf.CeilToInt(currentTime)} сек";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}