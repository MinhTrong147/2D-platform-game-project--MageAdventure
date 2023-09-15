using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    
    public GameObject player;
    public GameObject deathPanel;
    public void ReplayButton()
    {
        SceneManager.LoadScene("GameplayScene");
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
