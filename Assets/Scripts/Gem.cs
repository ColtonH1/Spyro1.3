/*
 * This script was made by Colton Henderson
 * 
 * This script controls the gems in the world
 * Things done in this script:
 *      control the particle effects
 *      set where Sparx where fly to pick-up the gem
 *      play the pick-up audio
 *      math for the way it sprials to the player
 *      keeps track of the gem count
 *      
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int value; //value of gem being picked up
    public GameObject trailParticle, touchParticles; //particle systems on gem
    bool hasTriggered; //keep track if all events have been triggered for the gem //firstTime;
    public Transform playerT; //Spyro
    public Vector3 firstPos { get; private set; } //original position
    public Vector3 sparxDestination { get; private set; } //where to send Sparx to pick up the gem
    float timer; //keep track of time to detroy gameobject after giving everything else time to play
    public static int gemCount = 0; //keep track of gem count
    //public bool start = false; 

    /* put back in later
    public SparxAnimatePickUpGem sparxAnim; //gameObject reference to SparxAnimatePickUpGem script
    */

    private Player spyrosScript; //gameObject reference to ThirdPersonMovement script
    public GameObject spyro; //Spyro's gameObject which holds the ThirdPersonMovement script that is needed

    //audio
    public AudioSource audioSource; //the necessary audio source
    public AudioClip pickUpGem; //the clip to play

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //to play the sound
        spyrosScript = spyro.GetComponent<Player>(); //to trigger the floating text
        if(touchParticles != null)
            touchParticles.SetActive(false);
        if(trailParticle != null)
            trailParticle.SetActive(false);
        firstPos = transform.position; //original position
        sparxDestination = GameObject.Find("Red Gems/Red Gem/Sparx Destination").transform.position; //where the sparx destination is located
    }
    private void OnTriggerEnter(Collider other)
    {
        //Spyro or Sparx could trigger this
        if(((other.CompareTag("Player") || other.CompareTag("Sparx"))) && !hasTriggered)
        {
            /* put back in later
            sparxAnim.SetStartAnim(true); //Have Sparx begin to pick up the gem
            */
            Invoke("playAudio", 0.2f); //play them gem audio shortly after being picked up
            /* put back in later
            spyrosScript.ShowFloatingText(); //trigger the floating text
            */
            gemCount++; //increase gem count
            //firstPos = transform.position; //original position

            trailParticle.SetActive(true); //trigger particles when flying to Spyro
            touchParticles.SetActive(false); //turn off particles for idle position
            hasTriggered = true; //all events for this gem has triggered
        }
    }


    void playAudio()
    {
        audioSource.PlayOneShot(pickUpGem); //invoked method to play audio
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Sparx"))
        {
            hasTriggered = false; //reset process
            Destroy(gameObject); //destory gameObject
            trailParticle.SetActive(false); //turn off particles
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTriggered)
        {
            if (timer > 1)
            {
                /* put back in later
                sparxAnim.SetStartAnim(false); //turn off animation after enough time for it to have been played
                */
                spyrosScript.gemFound = false; //tell Spyro's script the gem has been found and collected and so it is off
                gameObject.SetActive(false); //turn off gameObject
            }

            timer += Time.deltaTime; //keep track of time
            //variables for the sprialling to Spyro
            var mid = (playerT.position + firstPos) / 2;
            mid = new Vector3(mid.x, mid.y + 1.5f, mid.z);

            var upper = firstPos + new Vector3(0, 2, 0);

            transform.position = CalculateCubicBazierPosition(firstPos, upper, mid, playerT.position, timer); //postion is the flying to Spyro
        }
    }

    //calculate the way it sprials to Spyro
    Vector3 CalculateCubicBazierPosition(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((1 - t) * (1 - t) * (1 - t)) * p0 + 3 * ((1 - t) * (1 - t)) * t * p1 + 3 * (1 - t) * t * t * p2 + t * t * t * p3);
    }

    //gem count
    public int GetGems()
    {
        return gemCount;
    }
}
