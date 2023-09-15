using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngineInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseGameMenu : MonoBehaviour
{
    public int MainMenuScene;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public void OnPauseGameMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameIsPaused)
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
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ExitMainMenuGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(MainMenuScene);
    }
    public void QuitGame()
    {
            Application.Quit();
    }
}
