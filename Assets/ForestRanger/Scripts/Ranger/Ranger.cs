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
	[field: SerializeField] public int ID { get; private set; }
	private RangerHandler rHandler;
	private RangerOpenScene NewOpenScene;
	private RaycastHit2D hit;
	private RaycastHit2D Hit;
	private RaycastHit2D MissHit;
	private Vector3 TargetPosition;
	private bool IsMoving = false;
	private bool IsEventClose = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
	private Timer OpenSceneTimer;

	private List<MovementPoint> MovementPoints = new List<MovementPoint>();
	private List<MovementPoint> CurrentMovementPoints = new List<MovementPoint>();
	private List<MovementPoint> MinCurrentMovementPoints = new List<MovementPoint>();
	private MovementPoint CurrentMovementPoint;
	private MovementPoint FinalMovementPoint;
	private float VectorLengthSum = 0;
	private float MinVectorLengthSum = 100000;
	private bool IsFindWay = false;
	private int CurrentMovementPointCount = 0;


	private void Awake()
	{
		rHandler = GameObject.FindGameObjectWithTag("RangerHandler").GetComponent<RangerHandler>();

		foreach (var movementPoint in GameObject.FindGameObjectsWithTag("MovementPoint"))
		{
			MovementPoints.Add(movementPoint.GetComponent<MovementPoint>());
			if (movementPoint.GetComponent<MovementPoint>().ID == 0)
			{
				CurrentMovementPoint = movementPoint.GetComponent<MovementPoint>();
				transform.position = CurrentMovementPoint.transform.position;
			}
		}

		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		OpenSceneTimer = new Timer(0.2f);
		OpenSceneTimer.SetPause();
		OpenSceneTimer.OnTimerEnd += OpenScene;
	}

	public void ChangeCurrentPoint(int currentPointID)
	{
		foreach (var movementPoint in MovementPoints)
		{
			if (movementPoint.ID == currentPointID)
			{
				CurrentMovementPoint = movementPoint;
				transform.position = CurrentMovementPoint.transform.position;
				return;
			}
		}

	}

	private void OpenScene()
	{
		OpenSceneTimer.ResetTimer(true);
		NewOpenScene.LoadNewScene();
	}


	private void FixedUpdate()
	{
		OpenSceneTimer.Tick(Time.deltaTime);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (IsMoving) return;

			Hit = new RaycastHit2D();

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

			foreach (RaycastHit2D everyhit in hits)
			{
				if (everyhit.collider.gameObject.GetComponent<RangerOpenScene>() != null)
				{
					if (everyhit.collider.gameObject.GetComponent<RangerOpenScene>().ThisMovementPoint != null && everyhit.collider.gameObject.GetComponent<RangerOpenScene>().ThisMovementPoint.IsActive == true)
					{
						NewOpenScene = everyhit.collider.gameObject.GetComponent<RangerOpenScene>();
						FinalMovementPoint = everyhit.collider.gameObject.GetComponent<RangerOpenScene>().GetMovementPoint();
						hit = everyhit;
					}
					else if (everyhit.collider.gameObject.GetComponent<RangerOpenScene>().ThisMovementPoint == null) { }
						 else Debug.Log("Мне там делать нечего");
				}
			}

			if (FinalMovementPoint != null)
			{

				IsFindWay = false;
				CurrentMovementPointCount = 0;
				VectorLengthSum = 0;
				MinVectorLengthSum = 100000;
				CurrentMovementPoints.Clear();
				MinCurrentMovementPoints.Clear();
				CalculationMovementPoints(CurrentMovementPoint, FinalMovementPoint.ID);

				if (MinCurrentMovementPoints.Count == 0)
				{
					Debug.Log("Путь не найден");
					return;
				}

				TargetPosition = MinCurrentMovementPoints[0].ThisPosition;

				if (TargetPosition != transform.position)
				{
					IsMoving = true;

					if (animator != null)
					{
						animator.SetBool("IsWalk", true);
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

		if (/*IsMoving && */CurrentMovementPointCount != MinCurrentMovementPoints.Count) 
		{
			TargetPosition = MinCurrentMovementPoints[CurrentMovementPointCount].ThisPosition;

			transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Time.deltaTime * 3);

			if (spriteRenderer != null)
			{
				spriteRenderer.flipX = TargetPosition.x < transform.position.x;
			}

			if (transform.position == TargetPosition)
			{
				CurrentMovementPointCount++;

				if (CurrentMovementPointCount == MinCurrentMovementPoints.Count)
				{
					CurrentMovementPoint = MinCurrentMovementPoints[CurrentMovementPointCount - 1];
					FinalMovementPoint = null;
					IsMoving = false;

					if (hit.collider != null && hit.transform.gameObject.GetComponent<RangerOpenScene>() != null)
					{
						hit = new RaycastHit2D();
						OpenSceneTimer.Continue();
					}
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

	private void CalculationMovementPoints(MovementPoint currentPoint, int finalPointID)
	{
		if (currentPoint.ID == finalPointID && !IsMoving)
		{
			MinCurrentMovementPoints.Add(currentPoint);
			return;
		}

		currentPoint.IsActivated = true;

		for (int i = 0; i < currentPoint.Neighbors.Count; i++)         //Берется каждый сосед отдельной точки на карте
		{
			if (currentPoint.Neighbors[i].Neighbors.Count == 1 && currentPoint.Neighbors[i].ID != finalPointID || currentPoint.Neighbors[i].IsActivated) continue;

			CurrentMovementPoints.Add(currentPoint.Neighbors[i]);

			VectorLengthSum += currentPoint.NeighborsLength[i];


			if (currentPoint.Neighbors[i].ID == finalPointID)
			{
				IsFindWay = true;

				if (MinVectorLengthSum > VectorLengthSum)
				{
					MinVectorLengthSum = VectorLengthSum;
					MinCurrentMovementPoints = new List<MovementPoint>(CurrentMovementPoints);
				}
			}

			if (currentPoint.Neighbors[i].Neighbors.Count > 1 && currentPoint.Neighbors[i].ID != finalPointID) CalculationMovementPoints(currentPoint.Neighbors[i], finalPointID);


			VectorLengthSum -= currentPoint.NeighborsLength[i];
			CurrentMovementPoints.RemoveAt(CurrentMovementPoints.Count - 1);
		}

		currentPoint.IsActivated = false;
	}
}