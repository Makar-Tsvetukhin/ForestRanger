using System.Collections.Generic;
using UnityEngine;

public class Hut : MonoBehaviour
{
    private List<Placement> Placements = new List<Placement>();
    private MinigameState GameState;
    private int DoneCount;

    [SerializeField] private AudioSource completeSound;

    public IReadOnlyList<Placement> GetPlacements() => Placements;

    private void Start()
    {
        GameState = GetComponent<MinigameState>();
        DoneCount = 0;
        Placements.AddRange(transform.GetComponentsInChildren<Placement>());

        for (int i = 0; i < Placements.Count; i++)
            Placements[i].OnPiecePlaced += CheckDone;
    }

    public void CheckDone()
    {
        DoneCount = 0;
        foreach (var placement in Placements)
        {
            if (placement.CheckDone())
            {
                DoneCount++;
                if (completeSound != null) completeSound.Play();
            }
        }

        if (DoneCount == Placements.Count)
            GameState.WinGame();
    }
}
