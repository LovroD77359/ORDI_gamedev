using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SproutClimb : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private PlayerMovement movementScript;
    private HookPlantTrack hookPlantScript;
    private bool climbSuccess = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !movementScript.inputDisabled && movementScript.isGrounded != 0)
        {
            movementScript.inputDisabled = true;
            rb.velocity = Vector3.zero;

            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad biljke neki objekt koji nije player (jer ce biljka uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f),
                col => (!col.transform.CompareTag("Player") && !col.transform.CompareTag("Decoration") && !col.transform.CompareTag("GroundCollider")
                        && !col.transform.CompareTag("ScriptCollider"))) && movementScript.jumpingForbidden == 0 && movementScript.inMudOrWater == 0)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1, 0), 1f);        // trazimo collidere iznad  biljke NOTE: igrat se s ovim radijusom
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.CompareTag("HookPlant"))     // ako smo nasli hook plant
                    {
                        hookPlantScript = collider.GetComponent<HookPlantTrack>();
                        if (hookPlantScript.isGrown)         // i ako je hook plant narastao
                        {
                            // dohvati prihvatljivu poziciju za "sici" na pod pored hook planta
                            Vector3 climbPosition = centerPosition(collider.transform.position) + new Vector3(0, 1.625f, 0);
                            Debug.Log(climbPosition);
                            StartCoroutine(climb(climbPosition));      // ide climb
                            climbSuccess = true;
                        }
                        else { Debug.Log("hook plant nije narastao"); }
                        break;
                    }
                }
            }
            else { Debug.Log("iznad glave"); }
            if (!climbSuccess)
            {
                StartCoroutine(deny());
            }
            climbSuccess = false;
        }
    }

    // Funkcija koja ostvaruje penjanje
    IEnumerator climb(Vector3 climbPosition)
    {
        Vector3 climbDirection = (climbPosition - transform.position).normalized;
        climbDirection.y = 0;
        Quaternion rotateTo = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(climbDirection).eulerAngles.y - 180, 0));
        Quaternion initialRotation = transform.parent.rotation;
        float startTime = Time.time;
        float timeDif;
        while (Quaternion.Angle(transform.parent.rotation, rotateTo) > 5)
        {
            timeDif = Time.time - startTime;
            transform.parent.rotation = Quaternion.Slerp(initialRotation, rotateTo, timeDif * 2);     // slerp prema hook plantu (rotacija)
            yield return null;
        }
        transform.parent.rotation = rotateTo;

        // play climb animation
        animator.SetTrigger("isClimbing");
        yield return new WaitForSeconds(1.3f);
        Vector3 initialPosition = transform.parent.position;
        startTime = Time.time;
        while (Vector3.Distance(transform.parent.position, initialPosition + new Vector3(0, 2, 0)) > 0.1f)
        {
            timeDif = Time.time - startTime;
            transform.parent.position = Vector3.Lerp(initialPosition, initialPosition + new Vector3(0, 2, 0), timeDif * 2.25f);      // lerp prema gore
            yield return null;
        }
        transform.parent.position = initialPosition + new Vector3(0, 2, 0);

        // play dismount animation
        initialPosition += new Vector3(0, 2f, 0);
        climbPosition += climbDirection;
        Vector3 arcCenter = (initialPosition + climbPosition) * 0.5F - new Vector3(0, 1, 0);
        Vector3 startToCenter = initialPosition - arcCenter;
        Vector3 endToCenter = climbPosition - arcCenter;
        startTime = Time.time;
        while (Vector3.Distance(transform.parent.position, climbPosition) > 0.1f)
        {
            timeDif = Time.time - startTime;
            transform.parent.position = arcCenter + Vector3.Slerp(startToCenter, endToCenter, timeDif * 1.75f);     // slerp biljke na poziciju za silazak
            yield return null;
        }
        transform.parent.position = climbPosition;

        rb.velocity = Vector3.zero;
        movementScript.inputDisabled = false;
    }

    IEnumerator deny()
    {
        animator.SetTrigger("deny");
        yield return new WaitForSeconds(1);
        movementScript.inputDisabled = false;
    }

    // Funkcija koja centrira danu poziciju (na 0.5)
    Vector3 centerPosition(Vector3 position)
    {
        return new Vector3(
                Mathf.Round(position.x + 0.5f) - 0.5f,
                Mathf.Round(position.y + 0.5f) - 0.5f,
                Mathf.Round(position.z + 0.5f) - 0.5f
            );
    }
}

