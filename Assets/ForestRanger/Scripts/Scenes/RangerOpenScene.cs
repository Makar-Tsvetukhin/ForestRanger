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
	[field: SerializeField] public MovementPoint ThisMovementPoint { get; private set; }
	private MinigameHandler mHandler;
	private RangerHandler rHandler;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		rHandler = GameObject.FindGameObjectWithTag("RangerHandler").GetComponent<RangerHandler>();
		CheckMinigameEnding();
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
		/*if (mHandler.GetGameStatus(GameIndex) != 0 && !GetComponent<ChangeSprite>().IsOneTimeGame)
		{

		}
		else
		{*/
			rHandler.SaveCurrentMovementPoint(rHandler.ForestRangerID, ThisMovementPoint.ID);
			StartCoroutine(LoadScene(SceneName));
		//}
	}

	public string GetSceneName()
	{
		return SceneName;
	}

	public MovementPoint GetMovementPoint()
	{
		return ThisMovementPoint;
	}

	private void CheckMinigameEnding()
	{
		if (mHandler.GetGameStatus(GameIndex) != 0 && GameIsEnd != null)
		{
			if (ThisMovementPoint != null)
			{
				ThisMovementPoint.IsActive = false;
			}
		}
	}
}
