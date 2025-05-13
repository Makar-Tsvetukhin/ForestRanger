using UnityEngine;
using UnityEngine.UI;

public class PuzzleProgressUI : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Hut hut;

    private int totalPieces;
    private int placedPieces;

    private void Start()
    {
        if (progressSlider == null)
        {
            progressSlider = GetComponent<Slider>();
            if (progressSlider == null)
            {
                Debug.LogError("Слайдера нет");
                return;
            }
        }

        var placements = hut.GetComponentsInChildren<Placement>();
        totalPieces = placements.Length;
        placedPieces = 0;

        progressSlider.minValue = 0;
        progressSlider.maxValue = totalPieces;
        progressSlider.value = 0;

        foreach (var placement in placements)
        {
            placement.OnPiecePlaced += UpdateProgress;
        }
    }

    private void UpdateProgress()
    {
        placedPieces++;
        progressSlider.value = placedPieces;

        if (placedPieces >= totalPieces)
        {
            Debug.Log("Готово");
        }
    }

    private void OnDestroy()
    {
        if (hut != null)
        {
            var placements = hut.GetComponentsInChildren<Placement>();
            foreach (var placement in placements)
            {
                if (placement != null)
                {
                    placement.OnPiecePlaced -= UpdateProgress;
                }
            }
        }
    }
}