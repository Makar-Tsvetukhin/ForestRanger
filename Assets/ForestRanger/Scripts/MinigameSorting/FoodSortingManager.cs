using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodSortingManager : MonoBehaviour
{
	[field: SerializeField] private Slider progressSlider;
	[field: SerializeField] private GameObject resultsPanel;
	[field: SerializeField] private TextMeshProUGUI resultText;
	[field: SerializeField] private Button finishButton;
	[field: SerializeField] private MinigameState minigameState;
	[field: SerializeField] private int _totalItems = 8; 

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
		resultText.text = $"Правильно перенесено: {_correctlyMoved}/{_totalItems}\n";
        if (_correctlyMoved < _totalItems) resultText.text += "Задание не выполнено";
        else resultText.text += "Задание выполнено";


		if (_correctlyMoved == _totalItems)
        {
            if (!(minigameState.GetGameStatus() == 1))
            {
                minigameState.WinGame();
            }
        }
        else
        {
            if (!(minigameState.GetGameStatus() == -1))
            {
                minigameState.LoseGame();
            }
        }
    }
}