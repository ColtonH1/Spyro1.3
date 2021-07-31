/*
 * This script was made by Colton Henderson
 * 
 * This script keeps track of the character's health, both the player and the enemy
 * Through this script, damage can be taken or health can be given
 * The action to occur when the character dies is decided in this script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        Debug.Log(transform.name + " takes 1 damage.");
        if(currentHealth <= 0)
        {
            Die();
        }
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void AddHealth()
    {
        currentHealth++;
        Debug.Log(transform.name + " gains 1 health");
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    public virtual void Die()
    {
        //Die
        //This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
