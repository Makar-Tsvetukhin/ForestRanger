using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameHandler : MonoBehaviour
{
	[field: SerializeField] private string MainGameElementTag;

	private static MinigameHandler Instance;

	private List<bool> MiniGamesEnd = new List<bool>();
	private MinigameState GameState { get; set; }
	private int GameWins { get; set; }

	private void Awake()
	{

		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Instance.GameWins = 0;

			SceneManager.sceneLoaded += NewSceneLoad;

			for (int i = 0; i < 5; i++) MiniGamesEnd.Add(false);
		}
		else
		{
			Destroy(gameObject); // Удаляем дубликаты
		}
	}


	public void WinGame(int gameindex)
	{
		Instance.MiniGamesEnd[gameindex] = true;
	}

	public int GetGameWins()
	{
		int t = 0;

		for (int i = 0; i < Instance.MiniGamesEnd.Count; i++)
		{
			if (Instance.MiniGamesEnd[i]) t++;
		}

		return t;
	}

	private void NewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (GameObject.FindGameObjectWithTag(MainGameElementTag) == null) return;

		GameState = GameObject.FindGameObjectWithTag(MainGameElementTag).GetComponent<MinigameState>();

		if (MiniGamesEnd[GameState.GetGameIndex()]) GameState.LoadWinGame();
	}
}
