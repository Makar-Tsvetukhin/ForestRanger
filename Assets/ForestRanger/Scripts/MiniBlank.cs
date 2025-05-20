using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBlank : MonoBehaviour
{
	[field: SerializeField] private List<GameObject> DogameObjects = new List<GameObject>();
	[field: SerializeField] private List<GameObject> NotDogameObjects = new List<GameObject>();
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
		DogameObjects[index].SetActive(true);
	}

	public void CheckGameStatus()
	{
		if (DogameObjects.Count == 0 || mHandler == null || DogameObjects[0] == null) return;

		for (int i = 0; i < DogameObjects.Count; i++)
		{
			if (mHandler.GetGameStatus(i) == 1)
			{
				DogameObjects[i].SetActive(true);
				NotDogameObjects[i].SetActive(false);
			}
			else if (mHandler.GetGameStatus(i) == -1)
			{
				NotDogameObjects[i].SetActive(true);
				DogameObjects[i].SetActive(false);
			}
			else
			{
				if (DogameObjects[i].activeSelf || NotDogameObjects[i].activeSelf)
				{
					DogameObjects[i].SetActive(false);
					NotDogameObjects[i].SetActive(false);
				}
			}
		}
	}

	public void RestartGame()
	{
		if (DogameObjects.Count == 0 || mHandler == null) return;

		for (int i = 0; i < DogameObjects.Count; i++)
		{
			DogameObjects[i].SetActive(false);
			NotDogameObjects[i].SetActive(false);
		}
	}
}
