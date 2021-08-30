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
    public bool key;

    private void Awake()
    {
        currentHealth = maxHealth;
        key = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CharacterStats>().key)
        {
            Debug.Log("Safe died! and player has key == " + other.gameObject.GetComponent<CharacterStats>().key);
            Die(false, other.gameObject.GetComponent<CharacterStats>().key);
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        Debug.Log(transform.name + " takes 1 damage.");
        if(currentHealth <= 0)
        {
            if (ThirdPersonMovement.attacking == ThirdPersonMovement.Attacking.Charging)
                Die(true, key);
            else
                Die(false, key);
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

    public void AddKey()
    {
        key = true;
    }
    
    public virtual void Die(bool charging, bool key)
    {
        //Die
        //This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
