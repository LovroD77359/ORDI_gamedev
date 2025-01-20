using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Windows;

public class UnstuckRock : MonoBehaviour
{
    public int xMod;
    public int zMod;

    private Rigidbody rb;
    private int moveXCount = 0;
    private int moveZCount = 0;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (moveXCount != 0)
        {
            rb.MovePosition(transform.position + 0.5f * Time.fixedDeltaTime * new Vector3(1, 0, 0) * moveXCount);
        }
        if (moveZCount != 0)
        {
            rb.MovePosition(transform.position + 0.5f * Time.fixedDeltaTime * new Vector3(0, 0, 1) * moveZCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            moveXCount += xMod;
            moveZCount += zMod;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            moveXCount -= xMod;
            moveZCount -= zMod;
        }
    }
}
