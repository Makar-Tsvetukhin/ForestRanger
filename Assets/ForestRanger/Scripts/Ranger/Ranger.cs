using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Ranger : MonoBehaviour
{
	[field: SerializeField] private OpenCloseMagnifier Magnifier;
	private RangerOpenScene NewOpenScene;
	private Vector3 TargetPosition;
	private bool IsMoving = false;
	private bool IsEventClose = false;
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

			Debug.Log(hit.collider.gameObject);

			if (hit.collider.gameObject.GetComponent<RangerOpenScene>() != null)
			{
			
				IsEventClose = true;
				NewOpenScene = hit.collider.gameObject.GetComponent<RangerOpenScene>();

				TargetPosition = hit.transform.position;

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
