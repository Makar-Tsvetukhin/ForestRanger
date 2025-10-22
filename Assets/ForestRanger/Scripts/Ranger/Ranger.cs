using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Ranger : MonoBehaviour
{
	[field: SerializeField] private OpenCloseMagnifier Magnifier;
	private RangerOpenScene NewOpenScene;
	private RaycastHit2D hit;
	private RaycastHit2D Hit;
	private RaycastHit2D MissHit;
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
			Hit = new RaycastHit2D();

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

			foreach (RaycastHit2D everyhit in hits)
			{
				if (everyhit.collider.gameObject.GetComponent<RangerOpenScene>() != null) Hit = everyhit;
				else MissHit = everyhit;
			}

			if (Hit.collider == null) hit = MissHit;
			else hit = Hit;

			if (hit.collider.gameObject.GetComponent<RangerOpenScene>() != null && hit.collider.gameObject.GetComponent<RangerOpenScene>().GetSceneName() != "HutExamScene" && !Magnifier.IsMagnifierActive)
			{
			
				IsEventClose = true;
				if (hit.collider.gameObject.GetComponent<RangerOpenScene>() != null) NewOpenScene = hit.collider.gameObject.GetComponent<RangerOpenScene>();

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
