/*
 * This script was made by Colton Henderson
 * 
 * What this script will contain:
 * Health
 * Damage to enemy
 * Call to gems
 * Call to powerups
 * (more)
 */

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Finding a gem and attacking
    public static bool playerAttack; //if the player hit the attack button
    GameObject sparx; //Sparx's game object
    public bool gemFound; //if the gem was found
    public int gemCount;
    public Vector3 gemPosition { get; private set; } //where Sparx will go to collect the gem
    public GameObject sparxDestination;

    //Health
    public int health = 4;

    // Start is called before the first frame update
    void Start()
    {
        sparx = GameObject.Find("Player/Sparx"); //find Sparx's game object
        Debug.Log("We found " + sparx.name);
        playerAttack = false; //set the player to not attacking at first
        gemFound = false; //set to no gems being found at first
        sparxDestination = GameObject.Find("Player/Sparx Destination");
    }

    // Update is called once per frame
    void Update()
    {
        //if the player hits the attack button, then set attack to true so PlayerAttack script can be active
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Gem":
                Debug.Log("Gem gained");
                Gem gem = other.GetComponent<Gem>(); //set to the gem that was found
                Debug.Log("We have " + gem.GetGems() + " gems");
                gemFound = true; //Set that we found a gem so Sparx's script can be active
                gemPosition = gem.sparxDestination; //set where the position of the gem is
                AddToGemCount(1);
                break;

            case "Health":
                Debug.Log("Health gained");
                CharacterStats health = GetComponent<CharacterStats>();
                health.AddHealth();
                other.gameObject.SetActive(false);
                break;

            default:
                break;

        }

    }

    //add to gemCount the amount the gem was worth
    public void AddToGemCount(int i)
    {
        gemCount += i;
    }

    //a function for other scripts to find the gem count
    public int ReturnGemCount()
    {
        return gemCount;
    }
}
