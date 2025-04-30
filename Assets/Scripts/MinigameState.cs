using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameState : MonoBehaviour
{
	[field: SerializeField] private int GameIndex;
	private MinigameHandler mHandler { get; set; }
	private bool IsGameEnd { get; set; }

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();

		IsGameEnd = false;
	}

	public void WinGame()
	{
		if (IsGameEnd) return;

        IsGameEnd = true;
		mHandler.WinGame(GameIndex);
	}

	public void LoadWinGame()
	{
		IsGameEnd = true;
	}

	public bool GetGameStatus()
	{
		return IsGameEnd;
	}

	public int GetGameIndex()
	{
		return GameIndex;
	}
}
