using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCloseMagnifier : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject Magnifier;
	public bool IsMagnifierActive { get; private set; } = false;

	private void Start()
	{
		if (Magnifier != null) 
		Magnifier.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (Magnifier == null) return;
		Magnifier.SetActive(!Magnifier.activeSelf);
		IsMagnifierActive = !IsMagnifierActive;
	}
}
