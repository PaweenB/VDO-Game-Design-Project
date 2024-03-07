using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    [Header("Health Bar")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    
    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadespeed;
    
    private float durationTimer;

    public static bool isGameOver;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0);

        isGameOver = false;
    }

    void Update()
    {
        if(overlay.color.a > 0)
        {
            if(currentHealth < 30)
                return;
            durationTimer += Time.deltaTime;

            if(durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, tempAlpha);   
            }
        }

        if(isGameOver)
        {
         SceneManager.LoadSceneAsync(2);
        }
    }

    public void UpdateText(string promptMessege)
    {
        promptText.text = promptMessege;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 1);

        healthBar.SetHealth(currentHealth);
        durationTimer = 0;

        if(currentHealth <= 0)
        {
            isGameOver = true;
            SceneManager.LoadSceneAsync(2);
        }
    }

    public void Healing(int amount)
    {
        if(currentHealth < maxHealth)
        { 
            currentHealth += amount;
            healthBar.SetHealth(currentHealth);
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}