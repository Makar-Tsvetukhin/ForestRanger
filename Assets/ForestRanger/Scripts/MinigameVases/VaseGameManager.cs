using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class VaseGameManager : MonoBehaviour
{
	[field: SerializeField] private PlayableDirector CutScenes;
	[field: SerializeField] private PlayableAsset[] Timelines;
	[field: SerializeField] private Vase[] _Vase;
	[field: SerializeField] private GameObject SweetObject;
	private Sweet _Sweet;
	private int RandomMixing;

	private void Start()
	{
		_Sweet = SweetObject.GetComponent<Sweet>();

		_Sweet.InVase += StartMixing;

		_Vase[1].OnGameEnd += GameEnd;

	}

	private void StartMixing()
	{
		RandomMixing = Random.Range(1, 4);
		Debug.Log(RandomMixing);
		SweetObject.SetActive(false);

		CutScenes.playableAsset = Timelines[0];
		CutScenes.Play();
		CutScenes.stopped += ContinueMixing;
	}

	private void ContinueMixing(PlayableDirector playableDirector)
	{
		playableDirector.stopped -= ContinueMixing;

		if (RandomMixing == 1) CutScenes.playableAsset = Timelines[1];
		if (RandomMixing == 2) CutScenes.playableAsset = Timelines[2];
		if (RandomMixing == 3) CutScenes.playableAsset = Timelines[3];

		CutScenes.Play();
		CutScenes.stopped += EndMixing;
	}

	private void EndMixing(PlayableDirector playableDirector)
	{
		playableDirector.stopped -= EndMixing;

		if (RandomMixing == 1) CutScenes.playableAsset = Timelines[4];
		if (RandomMixing == 2) CutScenes.playableAsset = Timelines[5];
		if (RandomMixing == 3) CutScenes.playableAsset = Timelines[6];

		CutScenes.Play();
	}

	public void StartGame()
	{
		for (int i = 0; i < 3; i++)
		{
			_Vase[i].StartGame();
		}

		_Sweet.StartGame();
	}
	
	private void GameEnd()
	{
		Debug.Log("Ты угадал!");
	}

	public void Guess()
	{
		Debug.Log("Выбирай!");

		for (int i = 0; i < 3; i++)
		{
			_Vase[i].EndMixing();
		}

		_Sweet.EndMixing();

		SweetObject.SetActive(true);
	}
}
