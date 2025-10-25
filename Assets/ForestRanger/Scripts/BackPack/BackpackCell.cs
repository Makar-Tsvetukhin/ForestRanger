using UnityEngine;

public class BackpackCell : MonoBehaviour
{
	[field: SerializeField] private GameObject ItemPlace;
	private BackpackItem ThisItem;

	public void AddItem(GameObject item)
	{
		item.transform.SetParent(ItemPlace.transform, false);
	}
}
