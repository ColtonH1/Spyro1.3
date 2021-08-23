/*
 * This script is made by Colton Henderson
 * This script describes how the player attacks
 * There are functions for:
 *      When the player presses the attack button
 *      How to give damage
 *      Start and stop the fire breath
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //enum Attacking {Fire, Charging};
    public GameObject fireBreath; //prefab to instantiate
    public GameObject fireBreathLocation; //where to spawn
    public int flameDamage = 1;
    public float fireRate = .25f;
    public float flameRange = 5f;

    private WaitForSeconds flameDuration = new WaitForSeconds(.07f);
    private AudioSource fireAudio;
    private LineRenderer flameLine; //might delete later
    private float nextFire;

    private void Start()
    {
        flameLine = GetComponent<LineRenderer>();
        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ThirdPersonMovement.attacking is " + ThirdPersonMovement.attacking);
        //if player is set to attacking with fire
        if((ThirdPersonMovement.attacking == ThirdPersonMovement.Attacking.Fire) && ThirdPersonMovement.playerAttack && Time.time > nextFire)
        {
            FireAttack();
        }
        //if player is attacking by charging
        else if((ThirdPersonMovement.attacking == ThirdPersonMovement.Attacking.Charging) && ThirdPersonMovement.playerAttack && Time.time > nextFire)
        {
            ChargeAttack();
        }
        //else, there is no attack
        else
        {
            ThirdPersonMovement.playerAttack = false;
        }
    }

    private void ChargeAttack()
    {
        nextFire = Time.time + fireRate;
        GiveDamage();

        Debug.Log("Charge Attacking");
    }

    public void FireAttack()
    {
        nextFire = Time.time + fireRate;
        Fire();

        GiveDamage();

        ThirdPersonMovement.attacking = ThirdPersonMovement.Attacking.None;

        Debug.Log("Fire Attacking");
    }

    private void GiveDamage()
    {
        Vector3 rayOrigin = fireBreathLocation.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fireBreathLocation.transform.forward, out hit, flameRange))
        {
            CharacterStats health = hit.collider.GetComponent<CharacterStats>();

            if (health != null)
            {
                health.TakeDamage();
            }
        }
    }

    private void Fire()
    {
        //instantiate the object
        GameObject fireBreathGO = Instantiate(fireBreath, new Vector3(fireBreathLocation.transform.position.x, fireBreathLocation.transform.position.y, fireBreathLocation.transform.position.z), Quaternion.Euler(90, 0, 0));
        fireBreathGO.transform.parent = gameObject.transform; //set the parent
        fireBreathGO.transform.localScale = new Vector3(1, 1, 1); //set the scale
        ThirdPersonMovement.playerAttack = false; //tell thirdperson movement we are no longer attacking
        StartCoroutine(EndFireBreath(fireBreathGO, 5)); //wait 5 seconds before deleting
    }

    //wait to delete
    private IEnumerator EndFireBreath(GameObject gameObject, float waitTime)
    {
        fireAudio.Play();
        flameLine.enabled = true;
        yield return new WaitForSeconds(waitTime);
        flameLine.enabled = false;
        Destroy(gameObject);
    }
}
