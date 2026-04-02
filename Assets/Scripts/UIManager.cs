using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private const int maxEnergyPoints= 100;
    private const int maxBoostPoints= 100;

    public int currentEnergyPoints, currentBoostPoints = 0;

    public Image energyBar, scoreBoosterBar1, scoreBoosterBar2;

    public GameObject planetOne, planetTwo, planetThree;

    public bool planetSpawned = false;

    public TMP_Text planetLevel;
    public Material[] planetTransitionMaterial;
    public GameObject[] TransitioningPlanets;
    public GameObject[] PlaceHolderPlanets;
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
        scoreBoosterBar1.fillAmount = Mathf.Clamp01((float)currentBoostPoints / maxBoostPoints);
        scoreBoosterBar2.fillAmount = Mathf.Clamp01((float)currentBoostPoints / maxBoostPoints);
        PlanetSpawner();

        if(currentBoostPoints >= 100)
        {
            currentBoostPoints = 0;
        }
        else if(currentBoostPoints <= 0)
        {
            currentBoostPoints = 0;
        }

    }

    private void PlanetSpawner()
    {
        if (currentEnergyPoints >= 100 && !planetSpawned)
        {
            planetOne.SetActive(true);
            planetSpawned = true;
            currentEnergyPoints = 0;
            planetLevel.text = "Phase: 1";
        }
        if (currentEnergyPoints >= 100 && planetOne.activeSelf)
        {
            planetOne.SetActive(false);
            TransitioningPlanets[0].SetActive(true);
            PlaceHolderPlanets[0].SetActive(true);
            StartCoroutine(SummonPlanet(planetTwo));
            currentEnergyPoints = 0;
            planetLevel.text = "Phase: 2";
        }
        if (currentEnergyPoints >= 100 && planetTwo.activeSelf)
        {
            planetTwo.SetActive(false);
            planetThree.SetActive(true);
            currentEnergyPoints = 0;
            planetLevel.text = "Phase: 3";

        }
    }

    public void BoostHit() => AddPoints(20);

    public void BoostMiss() => AddPoints(-100);

    public void Hit() => AddScore(2);

    public void Miss() => AddScore(-1);

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
    IEnumerator SummonPlanet(GameObject planet)
    {
        yield return new WaitForSecondsRealtime(5f);
        planet.SetActive(true);
    }
}
