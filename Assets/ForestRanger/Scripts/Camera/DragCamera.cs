using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCamera : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[field: SerializeField] private Camera ThisCamera { get; set; }
	private Camera MainCamera { get; set; }
	private Vector3 StoneOffset { get; set; }

	private void Start()
	{
		MainCamera = Camera.main;
		ThisCamera.transform.position = GetStoneWorldPosition();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ThisCamera.transform.position = GetStoneWorldPosition();

		StoneOffset = transform.position - GetMouseScreenPosition();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 mouseWorldPosition = GetMouseScreenPosition();

		transform.position = mouseWorldPosition + StoneOffset;
		ThisCamera.transform.position = GetStoneWorldPosition();
	}

	public void OnEndDrag(PointerEventData eventData)
	{

	}

	private Vector3 GetMouseScreenPosition()
	{
		return Input.mousePosition;
	}

	private Vector3 GetMouseWorldPosition()
	{
		Vector3 mouseScreenPosition = Input.mousePosition;
		mouseScreenPosition.z = 0f;
		return MainCamera.ScreenToWorldPoint(mouseScreenPosition);
	}

	private Vector3 GetStoneWorldPosition()
	{
		return MainCamera.ScreenToWorldPoint(transform.position);
	}
}
