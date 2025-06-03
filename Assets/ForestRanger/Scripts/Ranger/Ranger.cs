using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Ranger : MonoBehaviour
{
	private RangerOpenScene NewOpenScene { get; set; }
	private Vector3 TargetPosition { get; set; }
	private bool IsMoving { get; set; } = false;
	private bool IsEventClose { get; set; } = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

			if (hit.collider.gameObject.GetComponent<RangerOpenScene>() != null)
			{
				IsEventClose = true;
				NewOpenScene = hit.collider.gameObject.GetComponent<RangerOpenScene>();

				TargetPosition = new Vector3(hit.point.x, hit.point.y, 0);

				if (TargetPosition != transform.position)
				{
					IsMoving = true;

					if (animator != null)
					{
						animator.SetBool("IsWalk", true);
					}

					if (spriteRenderer != null)
                    {
                        spriteRenderer.flipX = TargetPosition.x < transform.position.x;
                    }
                }
			}
			else
			{
				IsEventClose = false;
				IsMoving = false;

				animator.SetBool("IsWalk", false);
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
					IsEventClose = false;
				}
			}
		}
	}
}
