using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class forbidJumping : MonoBehaviour
    {
        public GameObject affectedPlayer;       // lik koji propadne
        private PlayerMovement playerMovement;
        public Collider objectCollider;
        private float ogJump;     
        private float ogSpeed;    
        private float newSpeed;   

        private void Start()
        {      
            objectCollider = GetComponent<Collider>();
            playerMovement = affectedPlayer.GetComponent<PlayerMovement>();
            ogJump = playerMovement.getJump();
            ogSpeed = playerMovement.getSpeed();
            newSpeed = 0.5f * ogSpeed;
        }

        private void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject == affectedPlayer)
            {
                //playerMovement.setSpeed(newSpeed);
                playerMovement.setJump(0);
            }
            //hrow new NotImplementedException();
        }

        private void OnCollisionExit(Collision collider)
        {
            if (collider.gameObject == affectedPlayer)
            {
                playerMovement.setSpeed(ogSpeed);
                playerMovement.setJump(ogJump); 
            }
        }

        // private void OnTriggerEnter(Collider collider)
        // {
        //     if (collider.gameObject == affectedPlayer)
        //     {
        //         //playerMovement.setSpeed(newSpeed);
        //         playerMovement.setJump(0);
        //     }
        //     //else{
        //     //    Physics.IgnoreCollision(collider, objectCollider, false);
        //     //}
        // }

        // private void OnTriggerExit(Collider collider)
        // {
        //     if (collider.gameObject == affectedPlayer)
        //     {
        //         playerMovement.setSpeed(ogSpeed);
        //         playerMovement.setJump(ogJump); 
        //     }
        //     //else{
        //     //    Physics.IgnoreCollision(collider, objectCollider, false);
        //     //}
        // }
    }
}