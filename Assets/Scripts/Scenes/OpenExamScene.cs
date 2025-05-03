using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpenExamScene : OpenScene
{
	[field: SerializeField] private GameObject GameIsEnd;
	[field: SerializeField] public int GameIndex;
	private MinigameHandler mHandler;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
	}

	protected override void CheckLoadScene()
	{
		if (mHandler.GetGameStatus(GameIndex) != 0)
		{
			GameIsEnd.SetActive(true);
			GameIsEnd.GetComponentInChildren<TextMeshProUGUI>().text = "Эта мини-игра закончена";
		}
		else StartCoroutine(LoadScene(SceneName));
	}
}