using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RangerOpenScene : MonoBehaviour
{
	[field: SerializeField] protected string SceneName;
	[field: SerializeField] private GameObject GameIsEnd;
	[field: SerializeField] public int GameIndex;
	private MinigameHandler mHandler;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
	}

	public void LoadNewScene()
	{
		CheckLoadScene();
	}


	private IEnumerator LoadScene(string scenename)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}

	private void CheckLoadScene()
	{
		if (mHandler.GetGameStatus(GameIndex) != 0)
		{
			GameIsEnd.SetActive(true);
			GameIsEnd.GetComponentInChildren<TextMeshProUGUI>().text = "Задание уже закончено";
		}
		else StartCoroutine(LoadScene(SceneName));
	}
}
