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

		if (minigameState.GetGameStatus() == 1) Text.text = "Задание выполено";
		else if (minigameState.GetGameStatus() == -1) Text.text = "Задание не выполнено";
		else
		{
			Text.text = "Задание не выполнено";
			minigameState.LoseGame();
		}
	}
}
