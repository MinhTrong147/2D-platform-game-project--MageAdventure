using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PH : MonoBehaviour
{

    //public
    public TMP_Text healthBarText;
    public float maxHealth = 100f;   
    public float currentHealth;
    public float invincibilityTime = 0.25f; 
    public HealthBar healthBar;
    [SerializeField] public GameObject deathPanel;
    public UnityEvent<float, Vector2> damageableHit;
    

    //private
    private Animator animator;
    private bool isInvincible;
    private float timeSinceHit = 0f;


    public bool Dead
    {
        get { return animator.GetBool(AnimationStrings.Dead); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarText.text = Mathf.Max(currentHealth, 0f).ToString("f2") + "/" + maxHealth.ToString("f2");
    }
    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
  
    }
    public void TakeDamage(float damage, Vector2 knockback)
    {

        if (!Dead && !isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            healthBarText.text = Mathf.Max(currentHealth, 0f).ToString("f2") + "/" + maxHealth.ToString("f2");
            animator.SetTrigger(AnimationStrings.Hurt);
            isInvincible = true;    
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }

        if (currentHealth <= 0)
        {
            Die();            
        }

    }
    public bool Heal(float healthRestore)
    {
        if (!Dead && currentHealth < maxHealth)
        {
            float maxHeal = Mathf.Max(maxHealth - currentHealth, 0);
            float actualHeal = Mathf.Min(maxHeal, healthRestore);
            currentHealth += actualHeal;
            healthBarText.text = currentHealth.ToString("f2") + "/" + maxHealth.ToString("f2");
            healthBar.SetHealth(currentHealth);
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;

    }
    public bool IncreaseHealth(float healthRise)
    {
        if (!Dead)
        {
            maxHealth += healthRise;
            healthBarText.text = currentHealth.ToString("f2") + "/" + maxHealth.ToString("f2");
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
            CharacterEvents.characterHealed(gameObject, healthRise);
            return true;
        }
        return false;
    }

    void Die()
    {
        animator.SetTrigger(AnimationStrings.Dead);
        StartCoroutine(ShowDeathPanelAfterDelay(2.0f));
    }

    IEnumerator ShowDeathPanelAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
