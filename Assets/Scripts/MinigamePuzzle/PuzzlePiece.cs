using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[field: SerializeField] private string Name;
	private Camera MainCamera { get; set; }
	private GameObject Placement {  get; set; }
	private Vector3 Offset { get; set; }
	private Vector3 StartPosition { get; set; }
	private bool InCollider { get; set; }
	private bool InPlace { get; set; }

	private void Start()
	{
		MainCamera = Camera.main;

		StartPosition = transform.position;

		InCollider = false;
		InPlace = false;
	}

	public string GetName()
	{
		return Name;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (InPlace) return;

		Offset = transform.position - GetMouseWorldPosition();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (InPlace) return;

		transform.position = GetMouseWorldPosition() + Offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (InPlace) return;

		if (InCollider)
		{
			transform.position = Placement.gameObject.transform.position;
			transform.rotation = Placement.gameObject.transform.rotation;
			InPlace = true;
		}
		else transform.position = StartPosition;
	}

	private Vector3 GetMouseWorldPosition()
	{
		return MainCamera.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Placement>() == null || collision.gameObject.GetComponent<Placement>().GetName() != Name) return;

		InCollider = true;
		Placement = collision.gameObject;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Placement>() == null || collision.gameObject.GetComponent<Placement>().GetName() != Name) return;

		InCollider = false;
		Placement = null;
	}
}
