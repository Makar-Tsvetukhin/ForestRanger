using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour
{
	[field: SerializeField] OpenCloseInputField CloseInputFeld;
	[field: SerializeField] private GameObject EndPanel;
	[field: SerializeField] private string RightAnswer;
	[field: SerializeField] private MinigameState GameState;
	[field: SerializeField] private TMP_InputField inputField;
	private TextMeshProUGUI EndPanelText;
	private bool IsLoad = false;

	private void Start()
	{

		EndPanelText = EndPanel.GetComponentInChildren<TextMeshProUGUI>();
		EndPanel.SetActive(false);

		inputField.text = "";
		inputField.onEndEdit.AddListener(CheckAnswer);

		if (GameState.GetGameStatus() == 0)
		{
			inputField.interactable = true;
			inputField.text = "";
			IsLoad = false;
		}

		if (GameState.GetGameStatus() == 1)
		{
			inputField.text = "1264";
			IsLoad = true;
			CheckAnswer("1264");
		}
	}

	private void CheckAnswer(string answer)
	{
		if (RightAnswer == answer)
		{
			if (!IsLoad)
			{
				EndPanel.SetActive(true);
				EndPanelText.text = "Сейф открыт";
			}
			inputField.interactable = false;
			GameState.WinGame();
		}
		else
		{
			EndPanel.SetActive(true);
			EndPanelText.text = "Неправильный пароль";
			inputField.text = "";
			GameState.LoseGame();
		}
		CloseInputFeld.OpenCloseField();
	}

	public void RestartGame()
	{
		inputField.interactable = true;
		inputField.text = "";
		IsLoad = false;
	}
}
