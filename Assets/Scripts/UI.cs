
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level_01");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
