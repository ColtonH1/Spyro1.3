using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
        //if player is set to attacking
        if(Player.playerAttack && Time.time > nextFire)
        {
            Attack();
        }
        else
        {
            Player.playerAttack = false;
        }
    }

    public void Attack()
    {
        nextFire = Time.time + fireRate;
        Fire();

        GiveDamage();

        Debug.Log("Attacking");
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
        Player.playerAttack = false; //tell thirdperson movement we are no longer attacking
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
