using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private string SceneName;


	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(LoadScene(SceneName));
	}

	private IEnumerator LoadScene(string scenename)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}