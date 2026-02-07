using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void SolarSystem()
    {
        SceneManager.LoadScene("Solar System");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
