using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunShine : MonoBehaviour
{
    private Animator animator;
    private HookPlantTrack hookPlantScript;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);        // trazimo collidere oko sunca NOTE: igrat se s ovim radijusom
            foreach (Collider collider in colliders)
            {
                if (collider.transform.CompareTag("HookPlant"))     // ako smo nasli hook plant
                {
                    hookPlantScript = collider.GetComponent<HookPlantTrack>();      // postavljamo mu isGrown na true
                    hookPlantScript.isGrown = true;
                }
            }
        }
    }
}

