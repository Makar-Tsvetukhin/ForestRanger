using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] protected string SceneName;


	public virtual void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("H");
		CheckLoadScene();
		Debug.Log("Z");
	}

	protected virtual void CheckLoadScene()
	{
		StartCoroutine(LoadScene(SceneName));
	}

	protected virtual IEnumerator LoadScene(string scenename)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}