using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsUI;

    [SerializeField] private InputActionReference pauseAction;

    public static bool gameIsPaused = false;

    private void OnEnable()
    {
        if(pauseAction != null)
        {
            pauseAction.action.performed += OnPausePerformed;
            pauseAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if(pauseAction != null)
        {
            pauseAction.action.performed -= OnPausePerformed;
            pauseAction.action.Disable();
        }
    }

    

    private void Start()
    {
        if(pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if(settingsUI != null)
            settingsUI.SetActive(false);

        Time.timeScale = 1f;
        gameIsPaused = false;
    }

   
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (pauseMenuUI != null)
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {  
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.musicSource.UnPause();
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.musicSource.Pause();
        gameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Settings()
    {
        //pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
