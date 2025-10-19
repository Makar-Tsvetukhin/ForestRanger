using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vase : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] private GameObject _Sweet;
	[field: SerializeField] private GameObject SweetPlace;
	[field: SerializeField] private bool IsRightVase;
	private Timer InUpTimer = new Timer(2);
	private Vector3 StartPosition;
	private bool IsGameStart = false;
	private bool IsUp = false;
	private bool IsDown = true;
	private bool IsSweetThere = false;

	public event Action OnGameEnd;

	private void Start()
	{
		StartPosition = transform.position;
		InUpTimer.OnTimerEnd += ResetTimer;
	}

	public void StartGame()
	{
		IsGameStart = true;
	}

	private void ResetTimer()
	{
		IsDown = true;
		IsUp = false;
		InUpTimer.ResetTimer(true);
	}

	public void EndMixing()
	{
		StartPosition = transform.position;
	}

	public GameObject GetSweetPlace()
	{
		return SweetPlace;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!IsGameStart || !IsRightVase) return;

		_Sweet.GetComponent<Sweet>().Guess();

		Debug.Log("Ваза поднимается");
		//IsDown = false;
		//IsUp = true;

		OnGameEnd?.Invoke();
	}

	private void Update()
	{
		InUpTimer.Tick(Time.deltaTime);

		if (IsUp && IsSweetThere)
		{
			/*transform.position = Vector3.MoveTowards(transform.position, new Vector3(StartPosition.x, StartPosition.y + 2, StartPosition.z), 0.01f);
			if (transform.position == new Vector3(StartPosition.x, StartPosition.y + 2, StartPosition.z)) InUpTimer.Continue();*/
			Debug.Log(_Sweet.transform.position);
			_Sweet.transform.position = Vector3.MoveTowards(_Sweet.transform.position, new Vector3(SweetPlace.transform.position.x, SweetPlace.transform.position.y + 2, SweetPlace.transform.position.z), 0.01f);
			if (_Sweet.transform.position == new Vector3(SweetPlace.transform.position.x, SweetPlace.transform.position.y + 2, SweetPlace.transform.position.z)) InUpTimer.Continue();
		}

		if (IsDown && IsSweetThere)
		{
			//transform.position = Vector3.MoveTowards(transform.position, StartPosition, 0.01f);
			_Sweet.transform.position = Vector3.MoveTowards(_Sweet.transform.position, SweetPlace.transform.position, 0.01f);
		}
	}
}
