using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject settings;

    private void Start()
    {
        settings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("PlanetSelection");
    }

    public void SolarSystem()
    {
        SceneManager.LoadScene("Solar System");
    }

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void X()
    {
        settings.SetActive(false);
    }
}
