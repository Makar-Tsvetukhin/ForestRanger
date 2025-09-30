using UnityEngine;
using System.Collections.Generic;

public class RandomPrefabSpawner : MonoBehaviour
{
	[field: SerializeField] private List<GameObject> foodPrefabs;
	[field: SerializeField] private List<GameObject> trashPrefabs;
	[field: SerializeField] private int totalItemsToSpawn = 10;
	[field: SerializeField] private float maxXOffset = 0.3f;
	[field: SerializeField] private float maxYOffset = 0.2f;

    public delegate void SpawnFinishedEvent(int spawnedCount);
    public event SpawnFinishedEvent OnSpawnFinished;

    public void SpawnPrefabs()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < totalItemsToSpawn; i++)
        {
            bool isFood = Random.value > 0.5f;
            List<GameObject> prefabList = isFood ? foodPrefabs : trashPrefabs;
            GameObject prefab = prefabList[Random.Range(0, prefabList.Count)];

            Vector2 randomOffset = new Vector2(
                Random.Range(-maxXOffset, maxXOffset),
                Random.Range(-maxYOffset, maxYOffset)
            );

            GameObject newObj = Instantiate(prefab, transform);
            newObj.transform.localPosition = randomOffset;
            newObj.tag = isFood ? "food" : "trash";
        }

        OnSpawnFinished?.Invoke(totalItemsToSpawn);
    }
}