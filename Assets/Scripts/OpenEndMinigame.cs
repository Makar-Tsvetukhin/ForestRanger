using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenEndMinigame : MonoBehaviour, IPointerClickHandler
{
	private GameObject EndMinigame { get; set; }
	private TextMeshProUGUI Text { get; set; }

	private void Start()
	{
		EndMinigame = GameObject.FindGameObjectWithTag("EndMinigame");
		Text = EndMinigame.GetComponentInChildren<TextMeshProUGUI>();

		EndMinigame.SetActive(false);
	}

	public void WinMinigame()
	{
		EndMinigame.SetActive(true);
		Text.text = "Задание выполнено!";
	}

	public void LoseMinigame()
	{
		EndMinigame.SetActive(true);
		Text.text = "Задание не выполнено";
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		LoseMinigame();
	}
}
