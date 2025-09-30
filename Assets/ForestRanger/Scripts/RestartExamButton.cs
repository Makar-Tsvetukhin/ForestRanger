using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartExamButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject EndImage;
	[field: SerializeField] private MiniBlank Blank;
	//[field: SerializeField] private InputFieldScript TextInputField;
	[field: SerializeField] private MinigameState MagnifierTask;
	private MinigameHandler mHandler;


	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		mHandler.RestartGame();
		Blank.RestartGame();
		//TextInputField.RestartGame();
		MagnifierTask.RestartGame();
		EndImage.SetActive(false);
	}
}
