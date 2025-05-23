using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckMinigameStatus : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private MinigameState minigameState;
	[field: SerializeField] private GameObject MinigameEnd;
	private TextMeshProUGUI Text;

	private void Start()
	{
		Text = MinigameEnd.GetComponentInChildren<TextMeshProUGUI>();
		MinigameEnd.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		MinigameEnd.SetActive(true);

		if (minigameState.GetGameStatus() == 1)
		{
			if (minigameState.GetGameIndex() != 1) Text.text = "Задание выполнено";
			minigameState.WinGame();
		}
		else if (minigameState.GetGameStatus() == -1)
		{
			if (minigameState.GetGameIndex() != 1) Text.text = "Задание не выполнено";
			minigameState.LoseGame();
		}
		else
		{
			if (minigameState.GetGameIndex() != 1) Text.text = "Задание не выполнено";
			minigameState.LoseGame();
		}
	}
}
