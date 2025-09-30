using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCloseInputField : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject InputField;
	public bool IsInputFieldActive { get; private set; } = false;

	private void Start()
	{
		InputField.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OpenCloseField();
	}

	public void OpenCloseField()
	{
		InputField.SetActive(!InputField.activeSelf);
		IsInputFieldActive = !IsInputFieldActive;
	}
}
