using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private const int maxEnergyPoints= 100;
    private const int maxBoostPoints= 100;

    public int currentEnergyPoints, currentBoostPoints = 0;

    public Image energyBar, scoreBoosterBar;

    public GameObject planetOne, planetTwo, planetThree;

    public bool planetSpawned = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        energyBar.fillAmount = Mathf.Clamp01((float)currentEnergyPoints / maxEnergyPoints);
        scoreBoosterBar.fillAmount = Mathf.Clamp01((float)currentBoostPoints / maxBoostPoints);
        PlanetSpawner();

        if(currentBoostPoints >= 100)
        {
            currentBoostPoints = 0;
        }

    }

    private void PlanetSpawner()
    {
        if (currentEnergyPoints >= 100 && !planetSpawned)
        {
            planetOne.SetActive(true);
            currentEnergyPoints = 0;
            planetSpawned = true;
        }
        if (currentEnergyPoints >= 100 && planetOne.activeSelf)
        {
            planetOne.SetActive(false);
            planetTwo.SetActive(true);
            currentEnergyPoints = 0;
        }
        if (currentEnergyPoints >= 100 && planetTwo.activeSelf)
        {
            planetTwo.SetActive(false);
            planetThree.SetActive(true);
            currentEnergyPoints = 0;
        }
    }

    public void BoostHit() => AddPoints(20);

    public void Hit() => AddScore(10);

    public void Miss() => AddScore(-5);

    private void AddScore(int baseScore)
    {
        currentEnergyPoints = currentEnergyPoints + baseScore;

        currentEnergyPoints = Mathf.Clamp(currentEnergyPoints, 0, maxEnergyPoints);
    }

    private void AddPoints(int basePoints)
    {
        currentBoostPoints = currentBoostPoints + basePoints;
        currentBoostPoints = Mathf.Clamp(currentBoostPoints, 0, maxBoostPoints);
    }
}
