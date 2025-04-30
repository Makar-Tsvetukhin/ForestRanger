using UnityEngine;

public class EdgeObjectAnchor : MonoBehaviour
{
    public enum Edge { Left, Right }
    public Edge edge;
    public float offset = 0.5f; 

    void UpdatePosition()
    {
        float horzExtent = Camera.main.orthographicSize * Camera.main.aspect;
        float xPos = edge == Edge.Left ?
            -horzExtent + offset :
            horzExtent - offset;

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    void Start() => UpdatePosition();
}