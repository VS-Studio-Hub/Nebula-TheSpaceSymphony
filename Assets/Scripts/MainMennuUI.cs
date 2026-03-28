using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMennuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;

    private void Start()
    {
        settings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void SolarSystem()
    {
        SceneManager.LoadScene("Solar System");
    }

    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void X()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }
}
