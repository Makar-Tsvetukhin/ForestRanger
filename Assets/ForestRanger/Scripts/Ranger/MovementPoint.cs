using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MovementPoint : MonoBehaviour
{
	[field: SerializeField] public int ID;
	[field: SerializeField] public List<MovementPoint> Neighbors;
	private List<Vector2> NeighborsVectors = new List<Vector2>();
	[field: NonSerialized] public Vector2 ThisPosition;
	[field: NonSerialized] public bool IsActivated = false;
	[field: NonSerialized] public bool IsActive = true;
	[field: NonSerialized] public List<float> NeighborsLength = new List<float>();

	private void Start()
	{
		for (int i = 0; i < Neighbors.Count; i++) NeighborsVectors.Add(Neighbors[i].ThisPosition);

		ThisPosition = transform.position;

		for (int i = 0; i < NeighborsVectors.Count; i++)
		{
			NeighborsLength.Add(VectorLengthCount(NeighborsVectors[i] - ThisPosition));
		}
	}

	private float VectorLengthCount(Vector2 vector)
	{
		return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
	}
}
