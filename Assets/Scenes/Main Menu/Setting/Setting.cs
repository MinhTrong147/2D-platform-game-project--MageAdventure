using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public TextMeshProUGUI numberText; //set collider Number
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value); //update value
    }
    //setting
    //volume 
    //number set scrollbar

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
