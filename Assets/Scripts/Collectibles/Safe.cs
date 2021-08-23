/*
 * This script is made by Colton Henderson
 * 
 * This script is to control the safe by what happens when it is opened: Die()
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : CharacterStats
{
    public float radius = 3f;

    public override void Die(bool charging, bool key)
    {
        if ((charging && !charging) || key)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                StartCoroutine(SetChildTag(i, .25f)); //wait .25 seconds before setting gem
            }
        }
        base.Die(charging, key);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}