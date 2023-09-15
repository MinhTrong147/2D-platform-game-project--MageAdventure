using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject damageBuffTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();

    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += (CharacterTookDamage);
        CharacterEvents.characterHealed += (CharacterHealed);
        CharacterEvents.characterBuffDamaged += (CharacterIncreaseDamage);
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= (CharacterTookDamage);
        CharacterEvents.characterHealed -= (CharacterHealed);
        CharacterEvents.characterBuffDamaged -= (CharacterIncreaseDamage);
    }
    public void CharacterTookDamage(GameObject character, float damageReceived)
    {
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = "- " + damageReceived.ToString("f2");
    }
    public void CharacterHealed(GameObject character, float healthRestored)
    {
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = "+ " + healthRestored.ToString("f2");
    }
    public void CharacterIncreaseDamage(GameObject character, float damageReceived)
    {
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageBuffTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = "Increase " + damageReceived.ToString("Damage");
    }
}
