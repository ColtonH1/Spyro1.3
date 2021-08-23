using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    enum PlayerMovement {Walking, Charging, Gliding, Flying};
    public enum Attacking {None, Fire, Charging};

    public static bool playerAttack; //if the player hit the attack button
    //variables for basic movement 
    public CharacterController controller; //controls the character's movement
    public Transform cam; //main camera

    public float speed = 6f; //speed of player
    float originalSpeed;

    public float turnSmoothTime = 0.1f; //how fast the camera turns
    float turnSmoothVelocity; //rate of camera turning

    //gravity
    Vector3 velocity; //rate of falling
    public float gravity = -9.81f; //real gravity
    public Transform groundCheck; //object to check if player is on ground
    public float groundDistance = 0.4f; //distance from ground
    public LayerMask groundMask; //terrain given the chosen mask
    bool isGrounded; //player is grounded or not
    public float jumpHeight = 3f; //how high the player can jump

    PlayerMovement playerMovement;
    public static Attacking attacking;

    private void Start()
    {
        originalSpeed = speed;
        playerMovement = PlayerMovement.Walking;
        attacking = Attacking.None;
        playerAttack = false; //set the player to not attacking at first
    }

    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            attacking = Attacking.Fire;
            playerAttack = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(playerMovement == PlayerMovement.Charging)
        {
            attacking = Attacking.Charging;
            playerAttack = true;
        }
    }

    private void Movement()
    {
        //variables for forward, side ways, and direction movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //gets the distance from the ground, which is known by the layer mask, and checks if the player is near the ground

        //running
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            speed *= 2;
            playerMovement = PlayerMovement.Charging;
            attacking = Attacking.Charging;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            speed = originalSpeed;
            playerMovement = PlayerMovement.Walking;
            attacking = Attacking.None;
        }

        //moving
        if (direction.magnitude >= 0.1f)
        {
            //smoothly rotates player
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //calculate movement direction
            controller.Move(moveDir.normalized * speed * Time.deltaTime); //moveplayer in direction desired
        }

        //gravity and jumping                                                                                            
        //if player is on the ground and not falling
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; //calculate increasing velocity of falling player
        controller.Move(velocity * Time.deltaTime); //move player down given velocity
    }
}
