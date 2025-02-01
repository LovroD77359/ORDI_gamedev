using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineRock : MonoBehaviour
{
    public Camera camera;

    private MeshRenderer meshRenderer;
    private Light light;
    private RaycastHit hit;
    private LayerMask mask;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        light = GetComponentInChildren<Light>();
        mask = LayerMask.GetMask("Ignore Raycast", "Sunce");
        mask = ~(mask);

    }

    void FixedUpdate()
    {
        if (Physics.Raycast(camera.transform.position, (transform.position - camera.transform.position).normalized, out hit, Vector3.Distance(camera.transform.position, transform.position), mask))
        {
            //Debug.Log(hit.collider.gameObject.name);
            meshRenderer.enabled = true;
            light.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
            light.enabled = false;
        }
    }
}
