using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Sweet : MonoBehaviour
{
	private GameObject SweetPlace;
	private Rigidbody2D Rigidbody;
	private Vector3 StartPosition;
	private bool IsGameStart = false;
	private bool IsUp = false;

	public event Action InVase;

	 
	private void Start()
	{
		StartPosition = transform.position;
		Rigidbody = GetComponent<Rigidbody2D>();
		Rigidbody.simulated = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponentInParent<Vase>() != null && collision.gameObject.GetComponent<Vase>() == null)
		{
			SweetPlace = collision.gameObject.GetComponentInParent<Vase>().GetSweetPlace();
			transform.position = SweetPlace.transform.position;
			Rigidbody.simulated = false;

			InVase?.Invoke();
		}
	} 

	public void EndMixing()
	{
		transform.position = SweetPlace.transform.position;
	}

	public void Guess()
	{
		IsUp = true;
	}

	public void StartGame()
	{
		Rigidbody.simulated = true;
		IsGameStart = true;
		IsUp = false;
	}

	public void RestartGame()
	{
		SweetPlace = null;
		IsGameStart = false;
		transform.position = StartPosition;
		Rigidbody.simulated = false;
		IsUp = false;
	}

	private void Update()
	{
		if (IsUp)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(SweetPlace.transform.position.x, StartPosition.y, SweetPlace.transform.position.z), 0.1f);
			if (transform.position == new Vector3(SweetPlace.transform.position.x, SweetPlace.transform.position.y + 2, SweetPlace.transform.position.z)) IsUp = false;
		}
	}
}
