using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject FadeOut;
    public int LevelScene;
    void OnTriggerEnter2D()
    {
        StartCoroutine(FadeOutLevel());
    }

    IEnumerator FadeOutLevel()
    {
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(LevelScene);
    }

    
}
