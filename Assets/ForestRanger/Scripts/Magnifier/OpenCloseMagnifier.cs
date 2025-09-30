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
		Magnifier.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Magnifier.SetActive(!Magnifier.activeSelf);
		IsMagnifierActive = !IsMagnifierActive;
	}
}
