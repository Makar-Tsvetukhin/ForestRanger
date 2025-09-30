using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBlank : MonoBehaviour
{
	[field: SerializeField] private List<GameObject> DoGameObjects = new List<GameObject>();
	[field: SerializeField] private List<GameObject> NotDoGameObjects = new List<GameObject>();
	private MinigameHandler mHandler;

	private void Start()
	{
		RestartGame();

		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		mHandler.OnUpdate += CheckGameStatus;

		CheckGameStatus();
	}

	public void MissionDone(int index)
	{
		DoGameObjects[index].SetActive(true);
	}

	public void CheckGameStatus()
	{
		if (DoGameObjects.Count == 0 || mHandler == null || DoGameObjects[0] == null) return;

		for (int i = 0; i < DoGameObjects.Count; i++)
		{
			if (mHandler.GetGameStatus(i) == 1)
			{
				DoGameObjects[i].SetActive(true);
				NotDoGameObjects[i].SetActive(false);
			}
			else if (mHandler.GetGameStatus(i) == -1)
			{
				NotDoGameObjects[i].SetActive(true);
				DoGameObjects[i].SetActive(false);
			}
			else
			{
				if (DoGameObjects[i].activeSelf || NotDoGameObjects[i].activeSelf)
				{
					DoGameObjects[i].SetActive(false);
					NotDoGameObjects[i].SetActive(false);
				}
			}
		}

	}

	public void RestartGame()
	{
		if (DoGameObjects.Count == 0 || mHandler == null) return;

		for (int i = 0; i < DoGameObjects.Count; i++)
		{
			DoGameObjects[i].SetActive(false);
			NotDoGameObjects[i].SetActive(false);
		}
	}
}
