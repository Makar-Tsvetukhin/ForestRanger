using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Ranger : MonoBehaviour
{
	private RangerOpenScene NewOpenScene { get; set; }
	private Vector3 TargetPosition { get; set; }
	private bool IsMoving { get; set; } = false;
	private bool IsEventClose { get; set; } = false;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<RangerOpenScene>() != null)
		{
			IsEventClose = true;
			//collision.gameObject.GetComponent<RangerOpenScene>().LoadNewScene();
			NewOpenScene = collision.gameObject.GetComponent<RangerOpenScene>();
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<RangerOpenScene>() != null)
		{
			IsEventClose = false;
			NewOpenScene = null;
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

			if (hit.collider != null)
			{
				TargetPosition = new Vector3(hit.point.x, hit.point.y, 0);
				if (TargetPosition != transform.position)
				{
					IsMoving = true;
				}
			}
		}

		if (IsMoving)
		{
			transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Time.deltaTime * 3);

			if (transform.position == TargetPosition)
			{
				IsMoving = false;
				if (IsEventClose)
				{
					NewOpenScene.LoadNewScene();
				}
			}
		}
	}
}
