using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRockWithPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private TrackVelocity trackedMovingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            trackedMovingPlatform = other.GetComponent<TrackVelocity>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            if (trackedMovingPlatform == other.GetComponent<TrackVelocity>())
            {
                trackedMovingPlatform = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (trackedMovingPlatform != null)
        {
            if (trackedMovingPlatform.velocity.x != 0)
            {
                rb.velocity = new Vector3(trackedMovingPlatform.velocity.x, rb.velocity.y, rb.velocity.z);
            }
            else if (trackedMovingPlatform.velocity.z != 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, trackedMovingPlatform.velocity.z);
            }
        }
    }
}
