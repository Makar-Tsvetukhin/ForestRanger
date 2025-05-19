using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
	[field: SerializeField] private Sprite NotDo;
	[field: SerializeField] private Sprite Do;
	[field: SerializeField] private Sprite Lose;
	[field: SerializeField] private int GameIndex;
	private SpriteRenderer spriteRender;
	private MinigameHandler mHandler;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		//mHandler.OnUpdate += CheckTask;

		spriteRender = GetComponent<SpriteRenderer>();
		spriteRender.sprite = NotDo;

		CheckTask();
	}

	private void CheckTask()
	{
		if (NotDo == null || Do == null) return;

		if (mHandler.GetGameStatus(GameIndex) == 1) IsDoTask();
		else if (mHandler.GetGameStatus(GameIndex) == -1) IsLoseTask();
		else IsNotDoTask();
	}

	public void IsDoTask()
	{
		spriteRender.sprite = Do;
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
