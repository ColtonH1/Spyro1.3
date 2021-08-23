/*
 * This script is made by Colton Henderson
 * 
 * This script is to control the tin can by what happens when it is opened: Die()
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinCan : CharacterStats
{
    public override void Die(bool charging, bool key)
    {
        if(charging)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                StartCoroutine(SetChildTag(i, .25f)); //wait 5 seconds before setting gem
            }
        }

    }

    private IEnumerator SetChildTag(int i, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(i).tag = "Gem";
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}

