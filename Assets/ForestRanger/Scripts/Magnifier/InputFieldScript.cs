using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour
{
	[field: SerializeField] private string RightAnswer;
	private MinigameState GameState { get; set; }
	private TMP_InputField inputField { get; set; }


	private void Start()
	{
		GameState = GetComponent<MinigameState>();

		inputField = GetComponent<TMP_InputField>();
		inputField.onEndEdit.AddListener(CheckAnswer);
	}

	private void CheckAnswer(string answer)
	{
		if (RightAnswer == answer)
		{
			Debug.Log("Ответ верный!");
			GameState.WinGame();
		}
		else
		{
			Debug.Log("Ответ неверный");
		}
	}
}
