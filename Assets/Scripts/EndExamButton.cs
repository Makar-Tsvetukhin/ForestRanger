using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndExamButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private MinigameHandler mHandler;
	[field: SerializeField] private GameObject EndImage;
	private TextMeshProUGUI Text { get; set; }



	private void Start()
	{
		Text = EndImage.GetComponentInChildren<TextMeshProUGUI>();
		Text.text = "";

		EndImage.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		EndImage.SetActive(true);
		Text.text = $"Ваша оценка: {mHandler.GetGameWins()}/5";
	}
}
