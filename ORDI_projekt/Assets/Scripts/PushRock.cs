using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Windows;

public class PushRock : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement movementScript;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            movementScript = other.GetComponent<PlayerMovement>();
            movementScript.isTouchingRock = true;

            if (movementScript.isDebuffed > 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            movementScript = other.GetComponent<PlayerMovement>();
            movementScript.isTouchingRock = false;

            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        }
    }
}
