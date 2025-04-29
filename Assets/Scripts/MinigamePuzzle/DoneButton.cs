using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoneButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private Hut hut;
	private MinigameState GameState { get; set; }

	private void Start()
	{
		GameState = hut.GetComponent<MinigameState>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (GameState.GetGameStatus())
		{
			Debug.Log("Халупа готова");
			return;
		}

		if (hut.CheckDone())
		{
			Debug.Log("Халупа готова");
			GameState.WinGame();
		}
		else Debug.Log("халупа не готова");
	}
}
