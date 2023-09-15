using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level001 : MonoBehaviour
{
    public GameObject fadeIn;

    void Start()
    {
        StartCoroutine(FadeInOff());
    }
    IEnumerator FadeInOff()
    {
        yield return new WaitForSeconds(0);
        fadeIn.SetActive(true);
    }
}
