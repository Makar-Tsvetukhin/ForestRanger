using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 _startPosition;
    private bool _isDragging = false;
    private bool IsOnTarget = false;

    void Start()
    {
        _startPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (gameObject.activeSelf)
            _isDragging = true;
    }

    void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("edibl") || collision.CompareTag("inedible"))
        {
            IsOnTarget = true;
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("edibl") || collision.CompareTag("inedible"))
		{
			IsOnTarget = false;
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
                bool isCorrect = (collider.CompareTag("edibl") && gameObject.CompareTag("food")) ||
                                (collider.CompareTag("inedible") && gameObject.CompareTag("trash"));

                gameObject.SetActive(false);
                FindObjectOfType<FoodSortingManager>().OnItemMoved(isCorrect);
                return;
            }
        }

        transform.position = _startPosition;
    }
}