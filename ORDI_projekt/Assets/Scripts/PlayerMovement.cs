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
        
        [SerializeField] float speed = 10;
        [SerializeField] float jump = 400;
        [SerializeField] Transform cam;
        [SerializeField] String playerTag;
        
        

        Boolean isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        }
        
        void Start()
        {
            
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            distanceToGround = col.bounds.extents.y;
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

            //movemenmt and jumping
            rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);

            if (playerTag == "Player1")
            {
                if (Input.GetKeyDown(KeyCode.RightShift) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
                {
                    rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                }
            }else if (playerTag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())//Input.GetButtonDown("Jump") && isGrounded()
                {
                    rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                }
            }
            
            
            
            if (movementDirection != Vector3.zero)
            {
                transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }

    }
//}