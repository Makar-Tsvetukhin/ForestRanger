using UnityEngine;
using System;

public class Placement : MonoBehaviour
{
    [field: SerializeField] private string Name;
    private bool IsDone { get; set; }
    public event Action OnPiecePlaced;

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
        OnPiecePlaced?.Invoke();
    }

    public bool CheckDone()
    {
        return IsDone;
    }
}