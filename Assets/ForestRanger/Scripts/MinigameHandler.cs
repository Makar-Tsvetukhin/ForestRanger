using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameHandler : MonoBehaviour
{
	[field: SerializeField] private string MainGameElementTag;
	[field: SerializeField] private RangerHandler RangerManager;
	private static MinigameHandler Instance;
	private List<int> MiniGamesEnd = new List<int>();
	private MinigameState GameState;

	public event Action OnUpdate;
	public event Action OnNewScene;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			SceneManager.sceneLoaded += NewSceneLoad;

			for (int i = 0; i < 8; i++) MiniGamesEnd.Add(0);
		}
		else
		{
			Destroy(gameObject); // Удаляем дубликаты
		}
	}


	public void WinGame(int gameindex)
	{
		Instance.MiniGamesEnd[gameindex] = 1;

		OnUpdate?.Invoke();
	}

	public void LoseGame(int gameindex)
	{
		Instance.MiniGamesEnd[gameindex] = -1;

		OnUpdate?.Invoke();
	}

	public int GetGameStatus(int gameindex)
	{
		return Instance.MiniGamesEnd[gameindex];
	}

	public int GetGameWins()
	{
		int t = 0;

		for (int i = 0; i < Instance.MiniGamesEnd.Count; i++)
		{
			if (Instance.MiniGamesEnd[i] == 1) t++;
		}

		return t;
	}

	public void RestartGame()
	{
		for (int i = 0; i < 5; i++) MiniGamesEnd[i] = 0;
	}

	private void NewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (GameObject.FindGameObjectWithTag(MainGameElementTag) == null) return;

		GameState = GameObject.FindGameObjectWithTag(MainGameElementTag).GetComponent<MinigameState>();

		if (MiniGamesEnd[GameState.GetGameIndex()] == 1) GameState.LoadWinGame();

		if (MiniGamesEnd[GameState.GetGameIndex()] == -1) GameState.LoadLoseGame();

		OnNewScene?.Invoke();
	}
}
