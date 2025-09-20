using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	[field: SerializeField] private AudioSource Music;

	private void Start()
	{
		Music.Play();
	}
}
