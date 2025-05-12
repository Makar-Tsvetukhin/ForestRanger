using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
	[field: SerializeField] private Sprite NotDo;
	[field: SerializeField] private Sprite Do;
	[field: SerializeField] private int GameIndex;
	private SpriteRenderer spriteRender;
	private MinigameHandler mHandler;

	private void Start()
	{
		mHandler = GameObject.FindGameObjectWithTag("MinigameHandler").GetComponent<MinigameHandler>();
		mHandler.OnUpdate += CheckTask;

		spriteRender = GetComponent<SpriteRenderer>();
		spriteRender.sprite = Do;

		CheckTask();
	}

	private void CheckTask()
	{
		if (mHandler.GetGameStatus(GameIndex) == 1) IsDoTask();
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
}
