using UnityEngine;

public class PlanetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject world;
    [SerializeField] private GameObject worldLevelTwo;
    [SerializeField] private GameObject worldLevelThree;

    private bool hasSpawned = false;
    private bool hasSpawner = false;

    void Update()
    {
        int currentScore = GameManager.instance.currentScore;

        if (currentScore >= 1500 && !hasSpawned)
        {
            world.SetActive(false);
            LevelTwo();
        }

        if (currentScore >= 2000 && !hasSpawner)
        {
            worldLevelTwo.SetActive(false);
            LevelThree();
        }


    }

    private void LevelTwo()
    {
        Instantiate(worldLevelTwo, transform.position, transform.rotation);
        hasSpawned = true; 
    }

    private void LevelThree()
    {
        Instantiate(worldLevelTwo, transform.position, transform.rotation);
        hasSpawner = true;
    }
}
