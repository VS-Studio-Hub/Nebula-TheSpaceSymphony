using UnityEngine;
using System.Collections;

public class NodeSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 0.5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int i = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
