using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Windows;

public class PushRock : MonoBehaviour
{
    private PlayerMovement movementScript;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            movementScript = other.GetComponent<PlayerMovement>();
            movementScript.isTouchingRock = true;
            //movementScript.speed *= 0.4f; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            movementScript = other.GetComponent<PlayerMovement>();
            movementScript.isTouchingRock = false;
            //movementScript.speed *= 2.5f;
        }
    }
}
