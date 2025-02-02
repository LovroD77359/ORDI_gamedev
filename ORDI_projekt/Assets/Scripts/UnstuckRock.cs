using System;
using System.Collections.Generic;
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
    private List<GameObject> debuffedPlayers;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        debuffedPlayers = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (rb.useGravity)
        {
            if (moveXCount != 0 || moveZCount != 0)
            {
                rb.AddForce(1.5f * (new Vector3(1, 0, 0) * moveXCount + new Vector3(0, 0, 1) * moveZCount), ForceMode.Acceleration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MudAndWater"))
        {
            DiscriminativeMediums mudAndWaterScript = other.GetComponent<DiscriminativeMediums>();
            debuffedPlayers.Add(mudAndWaterScript.affectedPlayer);
        }
        Debug.Log(debuffedPlayers.Count);
        if (other.transform.CompareTag("Player") && !debuffedPlayers.Contains(other.gameObject))
        {
            moveXCount += xMod;
            moveZCount += zMod;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MudAndWater"))
        {
            DiscriminativeMediums mudAndWaterScript = other.GetComponent<DiscriminativeMediums>();
            debuffedPlayers.Remove(mudAndWaterScript.affectedPlayer);
        }
        if (other.transform.CompareTag("Player") && !debuffedPlayers.Contains(other.gameObject))
        {
            moveXCount -= xMod;
            moveZCount -= zMod;
        }
    }
}
