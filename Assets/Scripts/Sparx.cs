/*
 * This script will have Sparx move from Spyro to the gem
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparx : MonoBehaviour
{
    GameObject spyro;
    Player spyroScript;
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        spyro = GameObject.Find("Player");
        spyroScript = spyro.GetComponent<Player>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(spyroScript.gemFound)
        {
            transform.position = Vector3.MoveTowards(transform.position, spyroScript.gemPosition, 10f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spyroScript.sparxDestination.transform.position, 10f * Time.deltaTime);
        }
    }
}
