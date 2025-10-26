using UnityEngine;
using UnityEngine.SceneManagement;

public class RangerHandler : MonoBehaviour
{
	private static RangerHandler Instance;
	private Ranger ForestRanger;
	public int ForestRangerID { get; private set; } = 0;
	private int RangerCurrentPointID = 0;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			SceneManager.sceneLoaded += NewSceneLoad;
		}
		else
		{
			Destroy(gameObject); // Удаляем дубликаты
		}
	}

	public void SaveCurrentMovementPoint(int rangerID, int currentPointID)
	{
		ForestRangerID = rangerID;
		Instance.RangerCurrentPointID = currentPointID;
	}

	private void NewSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (ForestRanger == null && GameObject.FindGameObjectWithTag("Player") != null)
		{
			ForestRanger = GameObject.FindGameObjectWithTag("Player").GetComponent<Ranger>();

			if (ForestRangerID != ForestRanger.ID)
			{
				ForestRangerID = ForestRanger.ID;
				RangerCurrentPointID = 0;
			}

			ForestRanger.ChangeCurrentPoint(Instance.RangerCurrentPointID);
		}
	}
}
