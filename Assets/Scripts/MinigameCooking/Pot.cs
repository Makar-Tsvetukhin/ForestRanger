using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

	[field: SerializeField] private List<Ingredient> NeededIngredients;

	private List<Ingredient> Ingredients = new List<Ingredient>();
	private MinigameState GameState { get; set; }
	private Timer CookingTimer { get; set; } = new Timer(5);
	private int RightIngredients { get; set; }
	private bool IsStartCooking { get; set; }

	private void Start()
	{
		GameState = GetComponent<MinigameState>();

		RightIngredients = 0;

		IsStartCooking = false;

		CookingTimer.OnTimerEnd += CookingResult;
	}

	public void AddIngredient(Ingredient ingredient)
	{
		Ingredients.Add(ingredient);
	}

	public void BeginCooking()
	{
		IsStartCooking = true;
	}

	public void PourIngredients()
	{
		Ingredients.Clear();

		for (int i = 0; i < NeededIngredients.Count; i++)
		{
			NeededIngredients[i].ReturnName();
		}
	}

	public void CookingResult()
	{
		if (GameState.GetGameStatus()) return;

		IsStartCooking = false;
		CookingTimer.ResetTimer(false);

		if (NeededIngredients.Count != Ingredients.Count)
		{
			UnsuccessfulCooking();
			return;
		}

		for (int i = 0;  i < Ingredients.Count; i++)
		{
			for (int j = 0; j < NeededIngredients.Count; j++)
			{
				if (NeededIngredients[i].GetName() == Ingredients[j].GetName())
				{
					RightIngredients++;

					NeededIngredients[i].SetName("Empty");

					continue;
				}
			}
		}

		if (RightIngredients == NeededIngredients.Count)
		{
			SuccessfulCooking();
			if (!GameState.GetGameStatus())
			{
				GameState.WinGame();
			}
		}
		else UnsuccessfulCooking();

		RightIngredients = 0;
	}

	private void UnsuccessfulCooking()
	{
		Debug.Log("Готовка не удалась");

		PourIngredients();
	}

	private void SuccessfulCooking()
	{
		Debug.Log("Готовка удалась");

		PourIngredients();
	}

	private void Update()
	{
		if (IsStartCooking)
		{
			CookingTimer.Tick(Time.deltaTime);
		}
	}
}
