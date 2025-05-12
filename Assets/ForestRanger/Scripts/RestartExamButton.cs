using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartExamButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject EndImage;
	[field: SerializeField] private MiniBlank Blank;
	private MinigameHandler mHandler;


	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		mHandler.RestartGame();
		Blank.RestartGame();
		EndImage.SetActive(false);
		gameObject.SetActive(false);
	}
}
