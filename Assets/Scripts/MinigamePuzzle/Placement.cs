using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
	[field: SerializeField] private string Name;
	private bool IsDone { get; set; }

	private void Start()
	{
		IsDone = false;
	}

	public string GetName()
	{
		return Name;
	}

	public void Done()
	{
		IsDone = true;
	}

	public bool CheckDone()
	{
		return IsDone;
	}
}
