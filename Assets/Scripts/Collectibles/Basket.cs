/*
 * This script was made by Colton Henderson
 * This script controls what happens when the basket is destroyed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : CharacterStats
{
    public override void Die(bool charging, bool key)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            StartCoroutine(SetChildTag(i, .25f)); //wait .25 seconds before setting gem
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
