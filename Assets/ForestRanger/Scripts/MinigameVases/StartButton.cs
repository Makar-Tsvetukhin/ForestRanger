using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private VaseGameManager gameManager;

	public void OnPointerClick(PointerEventData eventData)
	{
		gameManager.StartGame();
		gameObject.SetActive(false);
	}
}
