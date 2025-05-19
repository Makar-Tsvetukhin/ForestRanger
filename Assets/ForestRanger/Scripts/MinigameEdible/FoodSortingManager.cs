using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodSortingManager : MonoBehaviour
{
    public Slider progressSlider; 
    public GameObject resultsPanel;
    public TextMeshProUGUI resultText;
    public Button finishButton;
    public MinigameState minigameState;

    [SerializeField]
    private int _totalItems = 8; 

    private int _movedItems;
    private int _correctlyMoved;

    void Start()
    {
        minigameState = GetComponent<MinigameState>();
        finishButton.onClick.AddListener(CheckResults);
        resultsPanel.SetActive(false);

        progressSlider.minValue = 0;
        progressSlider.maxValue = _totalItems;
        progressSlider.value = 0;

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
        progressSlider.value = _movedItems;
    }

    public void CheckResults()
    {
        finishButton.gameObject.SetActive(false);
		resultsPanel.SetActive(true);
		resultText.text = $"Правильно перенесено: {_correctlyMoved}/{_totalItems}";

        if (_correctlyMoved == _totalItems)
        {
            //resultText.color = Color.green;
            if (!(minigameState.GetGameStatus() == 1))
            {
                minigameState.WinGame();
            }
        }
        else
        {
            //resultText.color = Color.yellow;
            if (!(minigameState.GetGameStatus() == -1))
            {
                minigameState.LoseGame();
            }
        }
    }
}