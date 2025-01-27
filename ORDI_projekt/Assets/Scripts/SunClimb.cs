using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunClimb : MonoBehaviour
{
    public GameObject sproutScriptCarrier;

    private Animator animator;
    private Rigidbody rb;
    private PlayerMovement movementScript;
    private PlayerMovement sproutMovementScript;
    private SproutGrow sproutGrow;
    private bool climbSuccess = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        movementScript = GetComponentInParent<PlayerMovement>();
        sproutMovementScript = sproutScriptCarrier.GetComponentInParent<PlayerMovement>();
        sproutGrow = sproutScriptCarrier.GetComponent<SproutGrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && movementScript.isGrounded != 0)
        {
            movementScript.inputDisabled = true;
            rb.velocity = Vector3.zero;

            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad sunca neki objekt koji nije player (jer ce sunce uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f),
                col => (!col.transform.CompareTag("Player") && !col.transform.CompareTag("Decoration") && !col.transform.CompareTag("GroundCollider")
                        && !col.transform.CompareTag("ScriptCollider"))) && movementScript.jumpingForbidden == 0 && movementScript.inMudOrWater == 0)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);        // trazimo collidere oko sunca NOTE: igrat se s ovim radijusom
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.CompareTag("Player") && collider.transform != transform.parent)     // ako je objekt player i nije sunce (sebe detektira)
                    {
                        if (sproutGrow.isGrown && centerPosition(sproutScriptCarrier.transform.position).y == centeredPosition.y)         // i ako je biljka narasla te ako su na istoj visini
                        {
                            // dohvati prihvatljivu poziciju za "sici" s biljke na pod
                            Vector3 climbPosition = sproutGrow.climbPosition;

                            if (climbPosition.y != -1)      // ako imamo validni climb position ide climb
                            {
                                StartCoroutine(climb(sproutScriptCarrier.transform.position, climbPosition));
                                climbSuccess = true;
                            }
                        }
                        else { Debug.Log("biljka nije narasla/visina nije ista"); }
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
    IEnumerator climb(Vector3 sproutPosition, Vector3 climbPosition)
    {
        sproutMovementScript.inputDisabled = true;

        Vector3 sproutDirection = (sproutPosition - transform.position).normalized;
        sproutDirection.y = 0;
        Quaternion rotateTo = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(sproutDirection).eulerAngles.y - 180, 0));
        Quaternion initialRotation = transform.parent.rotation;
        float startTime = Time.time;
        float timeDif;
        while (Quaternion.Angle(transform.parent.rotation, rotateTo) > 5)
        {
            timeDif = Time.time - startTime;
            transform.parent.rotation = Quaternion.Slerp(initialRotation, rotateTo, timeDif * 2);     // slerp prema biljci (rotacija)
            yield return null;
        }
        transform.parent.rotation = rotateTo;

        // play climb animation
        animator.SetTrigger("isClimbing");
        Vector3 initialPosition = transform.parent.position;
        Vector3 endPosition = initialPosition + sproutDirection * (Vector3.Distance(initialPosition, sproutPosition) - 0.25f) + new Vector3(0, 1, 0);
        Vector3 arcCenter = (initialPosition + endPosition) * 0.5f - new Vector3(0, 1, 0);
        Vector3 startToCenter = initialPosition - arcCenter;
        Vector3 endToCenter = endPosition - arcCenter;
        startTime = Time.time;
        while (Vector3.Distance(transform.parent.position, endPosition) > 0.1f)
        {
            timeDif = Time.time - startTime;
            transform.parent.position = arcCenter + Vector3.Slerp(startToCenter, endToCenter, timeDif * 1.5f);     // slerp sunca na biljku
            yield return null;
        }
        transform.parent.position = endPosition;

        startTime = Time.time;
        initialPosition = endPosition;
        endPosition = initialPosition + new Vector3(0, 1, 0);
        while (Vector3.Distance(transform.parent.position, endPosition) > 0.1f)
        {
            timeDif = Time.time - startTime;
            transform.parent.position = Vector3.Lerp(initialPosition, endPosition, timeDif * 0.5f);      // lerp prema gore
            yield return null;
        }
        transform.parent.position = endPosition;

        Vector3 climbDirection = (climbPosition - transform.position).normalized;
        climbDirection.y = 0;
        rotateTo = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(climbDirection).eulerAngles.y - 180, 0));
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, rotateTo, 1);           // brza rotacija prema climb positionu

        // play dismount animation
        initialPosition = endPosition;
        endPosition = climbPosition;
        arcCenter = (initialPosition + endPosition) * 0.5f - new Vector3(0, 1, 0);
        startToCenter = initialPosition - arcCenter;
        endToCenter = endPosition - arcCenter;
        startTime = Time.time;
        animator.SetTrigger("isJumping");
        while (Vector3.Distance(transform.parent.position, endPosition) > 0.1f)
        {
            timeDif = Time.time - startTime;
            transform.parent.position = arcCenter + Vector3.Slerp(startToCenter, endToCenter, timeDif * 1.5f);     // slerp sunca na poziciju za silazak
            yield return null;
        }
        transform.parent.position = endPosition;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        movementScript.inputDisabled = false;
        sproutMovementScript.inputDisabled = false;
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

