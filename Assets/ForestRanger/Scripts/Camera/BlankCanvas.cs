using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlankCanvas : MonoBehaviour
{
	private void Start()
	{
		SceneManager.sceneLoaded += NewSceneLoad;
	}

	private void NewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex != 0) gameObject?.SetActive(false);
		else if (!gameObject.activeSelf) gameObject?.SetActive(true);
	}
}
