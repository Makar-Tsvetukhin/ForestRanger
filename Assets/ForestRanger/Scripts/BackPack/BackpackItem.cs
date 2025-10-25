using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[field: SerializeField] public string Name { get; private set; }
	private Vector2 StartPosition;

	private void Start()
	{
		StartPosition = transform.position;
	}

	public void Looted()
	{

	}

	public void ParentIsChanged()
	{
		StartPosition = transform.position;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{

	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = StartPosition;
	}
}
