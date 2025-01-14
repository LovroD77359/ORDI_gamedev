using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


//materijal playera treba bit zero friction


//namespace DefaultNamespace
//{
    
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;
        private float distanceToGround;

        //ANIMACIJE KOD:
        private Animator animator;
        //private bool isRunning = false;
        
        [SerializeField] float speed = 10;
        [SerializeField] float jump = 400;
        [SerializeField] Transform cam;
        [SerializeField] String playerTag;
        
        

        Boolean isGrounded()
        {
            Vector3 raycastOrigin = transform.position - new Vector3(0, col.bounds.extents.y, 0);
            return Physics.Raycast(raycastOrigin, Vector3.down, 0.1f);


        }
        
        void Start()
        {
            
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            distanceToGround = col.bounds.extents.y;

            //ANIMACIJE KOD:
            animator = GetComponent<Animator>();
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
                if (Input.GetKeyDown(KeyCode.RightShift) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
                {
                    rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                    animator.SetTrigger("isJumping");
                    

                }

                //TEST SHINING ANIMACIJA 
                if (Input.GetKeyDown(KeyCode.LeftShift)){ //PROMIJENI KOJU TIPKU CE STISNUTI
                    animator.SetTrigger("isShining");
                }
            }else if (playerTag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
                {
                    rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                    animator.SetTrigger("isJumping");
                    
                }
            }

            if (!(isGrounded())){
                animator.SetTrigger("startFlying");
            }
            if(isGrounded()){
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