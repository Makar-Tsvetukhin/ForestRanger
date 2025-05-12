using System.Collections;
using UnityEngine;

public class NotDestroyOnLoad : MonoBehaviour
{
	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}
