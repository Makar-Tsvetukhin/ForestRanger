using UnityEngine;

public class IndicatorMove : MonoBehaviour
{
	[field: SerializeField] private float MaxY;
	private float Speed = 0.4f;


	private void Update()
	{
		transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time * Speed, MaxY) ,transform.localPosition.z);
	}
}