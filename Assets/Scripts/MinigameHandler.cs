using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
	private static MinigameHandler Instance;
	private int GameWins { get; set; }

	private void Awake()
	{

		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Instance.GameWins = 0;
		}
		else
		{
			Destroy(gameObject); // Удаляем дубликаты
		}

		//DontDestroyOnLoad(this);
	}

	private void Start()
	{
		
	}

	public void WinGame()
	{
		Instance.GameWins++;
	}

	public int GetGameWins()
	{
		return Instance.GameWins;
	}
}
