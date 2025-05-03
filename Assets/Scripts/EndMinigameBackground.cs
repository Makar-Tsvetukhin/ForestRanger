using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMinigameBackground : MonoBehaviour
{
	private void Start()
	{
		SceneManager.sceneLoaded += NewSceneLoad;
		gameObject?.SetActive(false);
	}

	private void NewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex >= 2 && scene.buildIndex <= 5) gameObject?.SetActive(true);
		else if (gameObject.activeSelf) gameObject?.SetActive(false);
	}
}
