using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackVelocity : MonoBehaviour
{
    public Vector3 velocity;

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        velocity.y = 0;
        previousPosition = transform.position;
    }
}
