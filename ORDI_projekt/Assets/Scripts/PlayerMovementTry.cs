using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

//materijal playera treba bit zero friction


//namespace DefaultNamespace
//{
    
    public class Player1MovementTry : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;
        private float distanceToGround;
        
        [SerializeField] float speed = 10;
        [SerializeField] float jump = 400;
        [SerializeField] Transform cam;
        
        

        Boolean isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        }
        
        void Start()
        {
            InputAssigments newInput = new InputAssigments();
            newInput.ActMap.Enable();
            newInput.ActMap.JumpSpace.performed += PlayerJump;

            newInput.ActMap.moveUp.performed += MoveUp;
            
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            
            distanceToGround = col.bounds.extents.y;
        }

        void MoveUp(InputAction.CallbackContext obj)
        {
            
            Debug.Log("Up pressed");
        }


        private void PlayerJump(InputAction.CallbackContext obj)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
            {
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                Debug.Log("Jump pressed");
            }
            
        }
        
        
        
        
        void Update()
        {
            
            //inputs
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            
            //camera direction
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            
            camForward.y = 0;
            camRight.y = 0;
            
            //relative camera directions
            Vector3 forwardRelative = verticalInput * speed * camForward;  
            Vector3 rightRelative = horizontalInput * speed * camRight;
            
            Vector3 movementDirection = forwardRelative + rightRelative;

            //movemenmt and jumping
            rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);

            // if (Input.GetKeyDown(KeyCode.Space) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
            // {
            //     
            //     // rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            // }
            
            if (movementDirection != Vector3.zero)
            {
                transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }

    }
//}