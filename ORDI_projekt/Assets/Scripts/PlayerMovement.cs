using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//materijal playera treba bit zero friction


namespace DefaultNamespace
{
    
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] float speed = 10;
        [SerializeField] float jump = 400;
        [SerializeField] Transform cam;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
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

            if (Input.GetButtonDown("Jump"))// && Mathf.Approximately(rb.velocity.y, 0))
            {
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            }
            
            if (movementDirection != Vector3.zero)
            {
                transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }

    }
}