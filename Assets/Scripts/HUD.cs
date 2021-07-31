/*
 * This script was made by Colton Henderson
 * 
 * This script is used to display the HUD numbers that correspond with each:
 *      Gem count
 *      Statue count
 *      Health
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI gemText, statueText, healthText;
    public Player player;
    public int statueCount, healthCount;

    // Update is called once per frame
    void Update()
    {
        gemText.text = player.ReturnGemCount().ToString();
        statueText.text = statueCount.ToString();
        healthText.text = healthCount.ToString();
        if(Input.GetKeyDown(KeyCode.P))
        {
            statueCount++;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthCount++;
        }
    }
}
