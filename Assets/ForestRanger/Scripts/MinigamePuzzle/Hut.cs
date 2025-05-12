using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hut : MonoBehaviour
{
	private List<Placement> Placements = new List<Placement>();
	private int DoneCount { get; set; }

	private void Start()
	{
		DoneCount = 0;

		foreach (var placement in transform.GetComponentsInChildren<Placement>())
		{
			Placements.Add(placement);
		}
	}

	public bool CheckDone()
	{
		for (int i = 0; i < Placements.Count; i++)
		{
			if (Placements[i].CheckDone()) DoneCount++;
		}

		if (DoneCount == Placements.Count) return true;
		else return false;
	}
}
