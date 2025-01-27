using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

    
public class PlayerMovement : MonoBehaviour
{
    public Collider col;
    public LayerMask groundLayer;
    public float speed = 10;
    public float jump = 400;
    public Transform cam;
    public string playerTag;
    [HideInInspector] public bool inputDisabled = false;
    [HideInInspector] public int jumpingForbidden = 0;
    [HideInInspector] public int isGrounded = 0;
    [HideInInspector] public bool isTouchingRock = false;
    [HideInInspector] public int isDebuffed = 0;
    [HideInInspector] public int inMudOrWater = 0;

    private Rigidbody rb;

    private float horizontalInput = 0;
    private float verticalInput = 0;
    //camera direction
    private Vector3 camForward;
    private Vector3 camRight;

    private SproutGrow sproutGrow;

    //ANIMACIJE KOD:
    private Animator animator;


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
        if (!other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            isGrounded++;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            jumpingForbidden++;
        }
        if (other.CompareTag("MudAndWater"))
        {
            inMudOrWater++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            isGrounded--;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            jumpingForbidden--;
        }
        if (other.CompareTag("MudAndWater"))
        {
            inMudOrWater--;
        }

        if (playerTag == "Player2" && isGrounded == 0 && (sproutGrow.isGrown || sproutGrow.isGrowing))
        {
            StartCoroutine(degrow());
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
                    if (jumpingForbidden == 0)
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
                    if (sproutGrow.isGrown || sproutGrow.isGrowing)
                    {
                        Debug.Log("space");
                        StartCoroutine(degrow());
                    }

                    if (jumpingForbidden == 0)
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

            //kod za animacije:
            bool isMoving = horizontalInput != 0 || verticalInput != 0;
            if (isMoving)
            {
                if (isTouchingRock)
                {
                    animator.SetBool("isPushing", true);
                }
                else
                {
                    animator.SetBool("isPushing", false);
                    animator.SetTrigger("startRunning");
                }

                if (playerTag == "Player2" && (sproutGrow.isGrown || sproutGrow.isGrowing))
                {
                    Debug.Log("move");
                    StartCoroutine(degrow());
                }
            }
            else
            {
                animator.SetBool("isPushing", false);
                animator.SetTrigger("stopRunning");
            }
        }

        if (isGrounded == 0)
        {
            animator.SetTrigger("startFlying");
        }
        if (isGrounded != 0)
        {
            animator.SetTrigger("stopFlying");
        }
    }

    public IEnumerator degrow()
    {
        sproutGrow.detectCol.enabled = false;
        inputDisabled = true;
        sproutGrow.isGrown = false;
        sproutGrow.isGrowing = false;
        sproutGrow.stemCol.enabled = false;
        animator.SetTrigger("isUngrowing");
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;
        jumpingForbidden--;
        inputDisabled = false;
    }
}