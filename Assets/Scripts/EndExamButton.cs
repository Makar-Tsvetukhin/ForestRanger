using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndExamButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject EndImage;
	[field: SerializeField] private GameObject RestartButton;
	private MinigameHandler mHandler;
	private TextMeshProUGUI Text { get; set; }



	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();

		Text = EndImage.GetComponentInChildren<TextMeshProUGUI>();
		Text.text = "";

		EndImage.SetActive(false);
		RestartButton.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		EndImage.SetActive(true);

		if (mHandler.GetGameWins() < 3)
		{
			RestartButton.SetActive(true);
			Text.text = $"Ваша оценка: {mHandler.GetGameWins()}/5\nК сожалению, вы не сдали экзамен";
		}
        else
        {
			Text.text = $"Ваша оценка: {mHandler.GetGameWins()}/5\nПоздравляем с успешной сдачей экзамена";
		}
    }
}
