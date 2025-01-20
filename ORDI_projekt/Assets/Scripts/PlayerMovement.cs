using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

    
public class PlayerMovement : MonoBehaviour
{
    public Collider col;
    public LayerMask groundLayer;
    [HideInInspector] public bool inputDisabled = false;
    [HideInInspector] public bool jumpingAllowed = true;
    [HideInInspector] public int isGrounded = 0;
        
    private Rigidbody rb;

    private float horizontalInput = 0;
    private float verticalInput = 0;
    //camera direction
    private Vector3 camForward;
    private Vector3 camRight;

    private SproutGrow sproutGrow;

    //ANIMACIJE KOD:
    private Animator animator;
    //private bool isRunning = false;
        
    [SerializeField] float speed = 10;
    [SerializeField] float jump = 400;
    [SerializeField] Transform cam;
    [SerializeField] String playerTag;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        camForward = cam.forward;
        camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;

        if (playerTag == "Player2")
        {
            sproutGrow = GetComponentInChildren<SproutGrow>();
        }

        //ANIMACIJE KOD:
        animator = GetComponent<Animator>();//komentirano za testiranje
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MudAndWater"))
        {
            isGrounded++;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater"))
        {
            jumpingAllowed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MudAndWater"))
        {
            isGrounded--;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater"))
        {
            jumpingAllowed = true;
        }
    }

    void Update()
    {
        if (!inputDisabled)
        {
            //inputs
            if (playerTag == "Player1")
            {
                horizontalInput = Input.GetAxis("Horizontal1");
                verticalInput = Input.GetAxis("Vertical1");     
            }
            else if (playerTag == "Player2")
            {
                horizontalInput = Input.GetAxis("Horizontal2");
                verticalInput = Input.GetAxis("Vertical2"); 
            }
            
        
            //relative camera directions
            Vector3 forwardRelative = verticalInput * speed * camForward;  
            Vector3 rightRelative = horizontalInput * speed * camRight;
            
            Vector3 movementDirection = forwardRelative + rightRelative;

            //movement and jumping
            rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);
            if (movementDirection.magnitude > 0.1f)
            {
                movementDirection = -movementDirection;
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }


            if (playerTag == "Player1")
            {
                if (Input.GetKeyDown(KeyCode.Return) && isGrounded != 0)
                {
                    if (jumpingAllowed)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                        animator.SetTrigger("isJumping");
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    }
                }
            } 
            else if (playerTag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded != 0)
                {
                    if (sproutGrow.isGrown)
                    {
                        sproutGrow.isGrown = false;     // NOTE: tu ide sprout degrow animacija
                        rb.constraints = RigidbodyConstraints.None;
                        rb.freezeRotation = true;
                    }

                    if (jumpingAllowed)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                        animator.SetTrigger("isJumping");
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);    
                    }
                }
            }

            if (isGrounded == 0)
            {
                animator.SetTrigger("startFlying");
            }
            if(isGrounded != 0)
            {
                animator.SetTrigger("stopFlying");
            }

            //kod za animacije:
            bool isMoving = horizontalInput != 0 || verticalInput != 0;
            if (isMoving)
            {
                animator.SetTrigger("startRunning");

                if (playerTag == "Player2" && sproutGrow.isGrown)
                {
                    sproutGrow.isGrown = false;     // NOTE: tu ide sprout degrow animacija
                    rb.constraints = RigidbodyConstraints.None;
                    rb.freezeRotation = true;
                }
            }
            else
            {
                animator.SetTrigger("stopRunning");
            }
        }
    }

    public float getSpeed() {
        return speed;
    }

    public float getJump() {
        return jump;
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void setJump(float newJump){
        jump = newJump;
    }
}