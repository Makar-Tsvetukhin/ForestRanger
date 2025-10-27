using UnityEngine;

public class WinOnCollision : MonoBehaviour
{
    [SerializeField] private Collider2D targetCollider;

    [SerializeField] private MinigameState minigameState;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == targetCollider)
        {
            minigameState.WinGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider == targetCollider)
        {
            minigameState.WinGame();
        }
    }
}
