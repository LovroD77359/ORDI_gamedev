using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


//materijal playera treba bit zero friction
//dodaj veliki colider za glavu

// kamjenje mora imat tag Rock
// playeri moraju imat tag Player

//namespace DefaultNamespace
//{
    
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        public Collider col;
        //public Collider col2;
        
        private List<Collider> colliders = new List<Collider>();
        
        private float distanceToGround;
        private Vector3 boxSize;
        private float maxDistance;
        private int isGrounded = 0;
        private bool jumpingAllowed = true;
        public LayerMask groundLayer;

        //public float XCoordOfColiderSphere;
        //public float YCoordOfColiderSphere;
        //public float ZCoordOfColiderSphere;
        

        //ANIMACIJE KOD:
        private Animator animator;
        //private bool isRunning = false;
        
        [SerializeField] float speed = 10;
        [SerializeField] float jump = 400;
        [SerializeField] Transform cam;
        [SerializeField] String playerTag;


        // Boolean jumpingAllowed()
        // {
        //     if (colliders.Count == 0)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }
        
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Rock") || other.CompareTag("Player") )
            {
                //colliders.Add(other);
                jumpingAllowed = false;
                isGrounded++;
            }
        
            if (other.CompareTag("Ground"))
            {
                isGrounded++;
            }
        }
        
        

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Rock") || other.CompareTag("Player"))
            {
                //colliders.Remove(other);
                jumpingAllowed = true;
                isGrounded--;

            }
            
            if (other.CompareTag("Ground"))
            {
                isGrounded--;
            }
        }
        

        // Boolean isGrounded()
        // {
        //     boxSize = col.bounds.extents;
        //     
        //     maxDistance = 0.1f;
        //     bool grounded = Physics.BoxCast(col.transform.position, 
        //                                     boxSize, 
        //                                     -transform.up, 
        //                                     transform.rotation, 
        //                                     maxDistance, 
        //                                     groundLayer);
        //     if (grounded)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }
        //
        void Start()
        {
            
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            distanceToGround = col.bounds.extents.y;

            //ANIMACIJE KOD:
             animator = GetComponent<Animator>();//komentirano za testiranje
        }


        void Update()
        {
            
            //inputs
            float horizontalInput = 0;
            float verticalInput = 0; 

            if (playerTag == "Player1")
            {
                horizontalInput = Input.GetAxis("Horizontal1");
                verticalInput = Input.GetAxis("Vertical1"); 
                
            }else if (playerTag == "Player2")
            {
                horizontalInput = Input.GetAxis("Horizontal2");
                verticalInput = Input.GetAxis("Vertical2"); 
            }
            
            
            //camera direction
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            
            camForward.y = 0;
            camRight.y = 0;
            
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
                if (Input.GetKeyDown(KeyCode.Return) && isGrounded != 0)//Input.GetButtonDown("Jump") && isGrounded()
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

                //TEST SHINING ANIMACIJA 
                if (Input.GetKeyDown(KeyCode.LeftShift)){ //PROMIJENI KOJU TIPKU CE STISNUTI
                    animator.SetTrigger("isShining");
                }
            }else if (playerTag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded != 0)//Input.GetButtonDown("Jump") && isGrounded()
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

            if (isGrounded == 0){
                animator.SetTrigger("startFlying");
            }
            if(isGrounded != 0){
                animator.SetTrigger("stopFlying");
            }

            //kod za animacije:
            bool isMoving = horizontalInput != 0 || verticalInput != 0;
            if (isMoving)
            {
                animator.SetTrigger("startRunning");
            }
            else
            {
            animator.SetTrigger("stopRunning");
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
//}