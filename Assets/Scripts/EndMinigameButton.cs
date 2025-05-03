using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndMinigameButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] public int GameIndex;
	private MinigameHandler mHandler;


	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		mHandler.LoseGame(GameIndex);
	}
}
