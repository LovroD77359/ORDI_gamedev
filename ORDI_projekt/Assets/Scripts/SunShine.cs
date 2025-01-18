using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunShine : MonoBehaviour
{
    private Animator animator;
    private Light light;
    private bool isShining = false;
    private HookPlantTrack hookPlantScript;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        light = GetComponentInParent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && !isShining)
        {
            animator.SetTrigger("isShining");
            StartCoroutine(shine());

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

    IEnumerator shine()
    {
        isShining = true;

        for (int i = 0; i < 40; i ++)
        {
            light.intensity += 0.0625f;
            light.range += 0.0125f;
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 40; i++)
        {
            light.intensity -= 0.0625f;
            light.range -= 0.0125f;
            yield return null;
        }

        isShining = false;
    }
}

