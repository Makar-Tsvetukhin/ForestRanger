using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
	[field: SerializeField] private GameObject Indicator;
	[field: SerializeField] private Sprite NotDo;
	[field: SerializeField] private Sprite Do;
	[field: SerializeField] private Sprite Lose;
	[field: SerializeField] private int GameIndex;
	[field: SerializeField] private SpriteRenderer spriteRender;
	private MinigameHandler mHandler;
	private CircleCollider2D Collider;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		mHandler.OnUpdate += CheckTask;

		Collider = GetComponent<CircleCollider2D>();

		CheckTask();
	}

	private void CheckTask()
	{
		if (mHandler == null || spriteRender == null) return;

		if (mHandler.GetGameStatus(GameIndex) == 1)
		{
			IsDoTask();
			Indicator.SetActive(false);
		}
		else if (mHandler.GetGameStatus(GameIndex) == -1)
		{
			IsLoseTask();
			Indicator.SetActive(false);
		}
		else IsNotDoTask();
	}

	public void IsDoTask()
	{
		spriteRender.sprite = Do;
		if (Do == null) Collider.enabled = false;
	}

	public void IsNotDoTask()
	{
		spriteRender.sprite = NotDo;
	}

	public void IsLoseTask()
	{
		spriteRender.sprite = Lose;
	}
}
