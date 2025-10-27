using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource pickUpSound;           
    [SerializeField] private AudioSource dropOnEdibleSound;     
    [SerializeField] private AudioSource dropOnInedibleSound;   

    private Vector3 _startPosition;
    private bool _isDragging = false;

    void Start()
    {
        _startPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (!gameObject.activeSelf) return;

        _isDragging = true;

        if (pickUpSound != null)
            pickUpSound.Play();
    }

    void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        if (!_isDragging) return;
        _isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("edibl") || collider.CompareTag("inedible"))
            {
                bool isCorrect =
                    (collider.CompareTag("edibl") && gameObject.CompareTag("food")) ||
                    (collider.CompareTag("inedible") && gameObject.CompareTag("trash"));

                if (collider.CompareTag("edibl") && dropOnEdibleSound != null)
                    dropOnEdibleSound.Play();
                else if (collider.CompareTag("inedible") && dropOnInedibleSound != null)
                    dropOnInedibleSound.Play();

                gameObject.SetActive(false);
                FindObjectOfType<FoodSortingManager>().OnItemMoved(isCorrect);
                return;
            }
        }

        transform.position = _startPosition;
    }
}
