using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Windows;

public class PushRock : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement movementScript;
    //Vector3 pushDirection = Vector3.zero;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    //private void FixedUpdate()
    //{
    //    rb.MovePosition(transform.parent.position + 0.5f * Time.fixedDeltaTime * pushDirection);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            movementScript = other.GetComponent<PlayerMovement>();
            movementScript.isTouchingRock = true;

            if (movementScript.isDebuffed > 0)
            {
                //pushDirection = transform.parent.position - other.transform.position;
                //pushDirection.y = 0;
                //pushDirection = pushDirection.normalized;
                //pushDirection = new Vector3(Mathf.Round(pushDirection.x), 0, Mathf.Round(pushDirection.z));
                //Debug.Log(pushDirection.ToString());
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

            if (movementScript.isDebuffed > 0)
            {
                //pushDirection = Vector3.zero;
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
