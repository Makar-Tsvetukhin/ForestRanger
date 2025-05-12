using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PourButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private Pot _Pot;

	public void OnPointerClick(PointerEventData eventData)
	{
		_Pot.PourIngredients();
	}
}
