using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystem : MonoBehaviour
{
    public void PlanetOne()
    {
        SceneManager.LoadScene("Planet01");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
