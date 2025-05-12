using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameState : MonoBehaviour
{
	[field: SerializeField] private int GameIndex;
	private MinigameHandler mHandler { get; set; }
	private int IsGameEnd { get; set; }

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();

		IsGameEnd = 0;
	}

	public void WinGame()
	{
		if (IsGameEnd == 1) return;

        IsGameEnd = 1;
		mHandler.WinGame(GameIndex);
	}

	public void LoseGame()
	{
		if (IsGameEnd == -1) return;

		IsGameEnd = -1;
		mHandler.LoseGame(GameIndex);
	}

	public void LoadWinGame()
	{
		IsGameEnd = 1;
	}

	public void LoadLoseGame()
	{
		IsGameEnd = -1;
	}

	public int GetGameStatus()
	{
		return IsGameEnd;
	}

	public int GetGameIndex()
	{
		return GameIndex;
	}
}
