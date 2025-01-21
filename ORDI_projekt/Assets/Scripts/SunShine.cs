using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunShine : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movementScript;
    private Light light;
    private bool isShining = false;
    private Animator hookPlantAnimator;
    private HookPlantTrack hookPlantScript;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();
        light = GetComponentInParent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && !isShining && movementScript.isGrounded != 0 && movementScript.jumpingForbidden == 0)
        {
            animator.SetTrigger("isShining");
            StartCoroutine(shine());

            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);        // trazimo collidere oko sunca NOTE: igrat se s ovim radijusom
            foreach (Collider collider in colliders)
            {
                if (collider.transform.CompareTag("HookPlant"))     // ako smo nasli hook plant
                {
                    StartCoroutine(growPlant(collider));
                }
            }
        }
    }

    IEnumerator shine()
    {
        isShining = true;
        movementScript.jumpingForbidden++;

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
        movementScript.jumpingForbidden--;
    }

    IEnumerator growPlant(Collider collider)
    {
        hookPlantAnimator = collider.GetComponent<Animator>();
        hookPlantAnimator.SetTrigger("isGrowing");
        yield return new WaitForSeconds(3);
        hookPlantScript = collider.GetComponent<HookPlantTrack>();      // postavljamo mu isGrown na true
        hookPlantScript.isGrown = true;
    }
}

