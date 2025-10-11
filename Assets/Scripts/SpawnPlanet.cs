using UnityEngine;

public class SpawnPlanet : MonoBehaviour
{
    [SerializeField] private GameObject world;
    [SerializeField] private Transform planetOne;
    [SerializeField] private GameObject worldLevelTwo;
    [SerializeField] private GameObject worldLevelThree;

    private bool hasSpawnedOne = false;
    private bool hasSpawnedTwo = false;
    private bool hasSpawnedThree = false;

    void Update()
    {
        int currentScore = GameManager.instance.currentScore;

        if (currentScore >= 1000 && !hasSpawnedOne)
        {
            SpawnPlanetOne();
        }
        if (currentScore >= 1500 && !hasSpawnedTwo)
        {
            SpawnPlanetTwo();
        }
        if (currentScore >= 2000 && !hasSpawnedThree)
        {
            SpawnPlanetThree();
        }

    }

    private void SpawnPlanetOne()
    {
        /*Instantiate(world, planetOne.position, planetOne.rotation);
        hasSpawnedOne = true;*/
        world.gameObject.SetActive(true);
    }
    private void SpawnPlanetTwo()
    {
        /*Instantiate(world, planetOne.position, planetOne.rotation);
        hasSpawnedTwo = true;*/
        world.gameObject.SetActive(false);
        worldLevelTwo.gameObject.SetActive(true);
    }
    private void SpawnPlanetThree()
    {
        /*Instantiate(world, planetOne.position, planetOne.rotation);
        hasSpawnedThree = true;*/
        worldLevelTwo.gameObject.SetActive(false);
        worldLevelThree.gameObject.SetActive(true);
    }
}
