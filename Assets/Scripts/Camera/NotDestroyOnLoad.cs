using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class NotDestroyOnLoad : MonoBehaviour
{
	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}
