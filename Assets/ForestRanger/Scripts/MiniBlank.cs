using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBlank : MonoBehaviour
{
	[field: SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
	private MinigameHandler mHandler { get; set; }

	private void Start()
	{
		RestartGame();

		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		mHandler.OnUpdate += CheckGameStatus;

		CheckGameStatus();
	}

	public void MissionDone(int index)
	{
		gameObjects[index].SetActive(true);
	}

	public void CheckGameStatus()
	{
		if (gameObjects.Count == 0 || mHandler == null) return;

		for (int i = 0; i < gameObjects.Count; i++)
		{
			if (mHandler.GetGameStatus(i) == 1) gameObjects[i].SetActive(true);
			else gameObjects[i].SetActive(false);
		}
	}

	public void RestartGame()
	{
		if (gameObjects.Count == 0 || mHandler == null) return;

		for (int i = 0; i < gameObjects.Count; i++)
		{
			gameObjects[i].SetActive(false);
		}
	}
}
