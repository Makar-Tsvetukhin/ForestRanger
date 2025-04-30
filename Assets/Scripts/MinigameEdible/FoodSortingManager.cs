using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodSortingManager : MonoBehaviour
{
    public TextMeshProUGUI progressText;
    public GameObject resultsPanel;
    public TextMeshProUGUI resultText;
    public Button finishButton;
    public RandomPrefabSpawner spawner;
    public Button restartButton;
    public MinigameState minigameState;

    private int _totalItems;
    private int _movedItems;
    private int _correctlyMoved;

    void Start()
    {
        minigameState=GetComponent<MinigameState>();
        finishButton.onClick.AddListener(CheckResults);
        restartButton.onClick.AddListener(RestartScene);
        resultsPanel.SetActive(false);
        spawner.OnSpawnFinished += HandleSpawnFinished;
        spawner.SpawnPrefabs();
    }

    private void HandleSpawnFinished(int spawnedCount)
    {
        _totalItems = spawnedCount;
        UpdateProgress();
    }

    public void OnItemMoved(bool isCorrect)
    {
        _movedItems++;
        if (isCorrect) _correctlyMoved++;
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        progressText.text = $"{_movedItems}/{_totalItems} перенесено";
    }

    public void CheckResults()
    {
        finishButton.gameObject.SetActive(false);
        resultText.text = $"Правильно перенесено: {_correctlyMoved}/{_totalItems}";
        resultsPanel.SetActive(true);

        if (_correctlyMoved == _totalItems)
        {
            resultText.color = Color.green;
            if (!minigameState.GetGameStatus())
            {
                minigameState.WinGame();
            }
        }
        else
        {
            resultText.color = Color.yellow;
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}