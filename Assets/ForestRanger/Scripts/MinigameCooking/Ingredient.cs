using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private string Name;
	[field: SerializeField] private GameObject DestinationPoint;
	[field: SerializeField] private GameObject ThisIngredient;
	private GameObject CreatedIngredient {  get; set; }
	private string StartName { get; set; }
	private bool IsMoving { get; set; } = false;

	private void Start()
	{
		StartName = Name;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		CreatedIngredient = Instantiate(/*gameObject*/ThisIngredient, transform.position/*UIPointToScene()*/, Quaternion.identity);
		CreatedIngredient.GetComponent<SpriteRenderer>().sortingOrder = 5;
		CreatedIngredient.GetComponent<Ingredient>().StartMove();
	}

	public string GetName()
	{
		return Name;
	}

	public void SetName(string newname)
	{
		Name = newname;
	}

	public void ReturnName()
	{
		Name = StartName;
	}

	public void StartMove()
	{
		IsMoving = true;
	}

	private void Update()
	{
		if (IsMoving)
		{
			gameObject.transform.position = Vector2.MoveTowards(transform.position, DestinationPoint.transform.position, 10 * Time.deltaTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "MiniGame")
		{
			collision.gameObject.GetComponent<Pot>().AddIngredient(this);
			Destroy(gameObject);
		}
	}

	private Vector3 UIPointToScene()
	{
		return Camera.main.ScreenToWorldPoint(transform.position);
	}
}