using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pot : MonoBehaviour
{
	[field: SerializeField] private GameObject EndImage;
	[field: SerializeField] private List<Ingredient> NeededIngredients;

	private List<Ingredient> Ingredients = new List<Ingredient>();
	private MinigameState GameState;
	private Timer CookingTimer = new Timer(3);
	private TextMeshProUGUI EndText;
	private int RightIngredients = 0;
	private bool IsStartCooking = false;

	private void Start()
	{
		GameState = GetComponent<MinigameState>();

		EndText = EndImage.GetComponentInChildren<TextMeshProUGUI>();

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
		if (GameState.GetGameStatus() == 1) return;

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
			if (!(GameState.GetGameStatus() == 1))
			{
				GameState.WinGame();
			}
		}
		else UnsuccessfulCooking();

		RightIngredients = 0;
	}

	private void UnsuccessfulCooking()
	{
		EndImage.SetActive(true);
		EndText.text = "Готовка не удалась\nЗадание не выполнено";

		PourIngredients();
	}

	private void SuccessfulCooking()
	{
		EndImage.SetActive(true);
		EndText.text = "Готовка удалась\nЗадание выполнено";

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
