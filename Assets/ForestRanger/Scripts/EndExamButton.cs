using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndExamButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject EndImage;
	[field: SerializeField] private GameObject RestartButton;
	[field: SerializeField] private GameObject LoseField;
	[field: SerializeField] private GameObject WinField;
	private MinigameHandler mHandler;
	private TextMeshProUGUI LoseText;
	private TextMeshProUGUI WinText;



	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();

		LoseText = LoseField.GetComponentInChildren<TextMeshProUGUI>();
		LoseText.text = "";

		WinText = WinField.GetComponentInChildren<TextMeshProUGUI>();
		WinText.text = "";

		LoseField.SetActive(false);
		WinField.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{

		if (mHandler.GetGameWins() < 3)
		{
			LoseField.SetActive(true);
			LoseText.text = $"Эх, дружок… Давай заново. И слушай, что говорю!";
		}
        else
        {
            WinField.SetActive(true);

			if (mHandler.GetGameWins() == 5) WinText.text = $"Ого! Да ты прирождённый егерь! Держи документы — избушка и ружьё твои.";
			if (mHandler.GetGameWins() == 4) WinText.text = $"Неплохо! Пару ошибок — не смертельно, но в лесу будь внимательнее.";
			if (mHandler.GetGameWins() == 3) WinText.text = $"Ладно… Проходи. Но учи матчасть, а то останешься без ужина.";
		}
    }
}