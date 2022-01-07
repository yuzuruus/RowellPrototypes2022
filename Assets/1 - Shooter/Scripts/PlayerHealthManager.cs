using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth = 10;
    int currentHealth;
    public TMP_Text healthUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void UpdateHealthUI()
    {
        if (healthUI)
        {
            healthUI.text = "Health: " + currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        CheckDeath();
    }


    void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("You are dead!");
        }
    }
}
