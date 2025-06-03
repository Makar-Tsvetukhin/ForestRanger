using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingButton : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private Pot _Pot;

	public void OnPointerClick(PointerEventData eventData)
	{
		_Pot.BeginCooking();
	}
}
