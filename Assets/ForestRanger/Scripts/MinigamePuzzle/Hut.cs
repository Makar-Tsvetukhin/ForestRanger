using System.Collections.Generic;
using UnityEngine;

public class Hut : MonoBehaviour
{
    private List<Placement> Placements = new List<Placement>();
    private int DoneCount { get; set; }

    public IReadOnlyList<Placement> GetPlacements() => Placements;

    private void Start()
    {
        DoneCount = 0;
        Placements.AddRange(transform.GetComponentsInChildren<Placement>());
    }

    public bool CheckDone()
    {
        DoneCount = 0;
        foreach (var placement in Placements)
        {
            if (placement.CheckDone()) DoneCount++;
        }
        return DoneCount == Placements.Count;
    }
}