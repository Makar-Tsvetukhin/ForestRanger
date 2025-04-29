using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
	[field: SerializeField] private string Name;



	public string GetName()
	{
		return Name;
	}
}
