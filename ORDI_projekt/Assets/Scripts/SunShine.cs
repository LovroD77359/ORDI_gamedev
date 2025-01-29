using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunShine : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private PlayerMovement movementScript;
    private Light light;
    private bool isShining = false;
    private Animator hookPlantAnimator;
    private HookPlantTrack hookPlantScript;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        movementScript = GetComponentInParent<PlayerMovement>();
        light = GetComponentInParent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.RightControl)) && !movementScript.inputDisabled && !isShining && movementScript.isGrounded != 0)
        {
            if (movementScript.jumpingForbidden == 0 && movementScript.inMudOrWater == 0)
            {
                movementScript.inputDisabled = true;
                rb.velocity = Vector3.zero;

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
            else
            {
                StartCoroutine(deny());
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
        movementScript.inputDisabled = false;
    }

    IEnumerator growPlant(Collider collider)
    {
        hookPlantAnimator = collider.GetComponent<Animator>();
        hookPlantAnimator.SetTrigger("isGrowing");
        yield return new WaitForSeconds(3);
        hookPlantScript = collider.GetComponent<HookPlantTrack>();      // postavljamo mu isGrown na true
        hookPlantScript.isGrown = true;
    }

    IEnumerator deny()
    {
        animator.SetTrigger("deny");
        yield return new WaitForSeconds(1);
        movementScript.inputDisabled = false;
    }
}

