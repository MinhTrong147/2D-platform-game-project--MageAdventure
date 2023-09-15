using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int gameStartScene;

    // start game
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
    //exit game
    public void ExitGame()
    {
        Application.Quit();
    } 

}
