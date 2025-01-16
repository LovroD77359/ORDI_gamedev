using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunClimb : MonoBehaviour
{
    public GameObject sprout;
    public bool isClimbing = false;

    private bool climbSuccess = false;
    private Animator animator;
    private SproutGrow sproutGrow;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sproutGrow = sprout.GetComponent<SproutGrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad sunca neki objekt koji nije player (jer ce sunce uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f), col => !col.transform.CompareTag("Player")))
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);        // trazimo collidere oko sunca NOTE: igrat se s ovim radijusom
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.CompareTag("Player") && collider.transform != transform)     // ako je objekt player i nije sunce (sebe detektira)
                    {
                        if (sproutGrow.isGrown)         // i ako je biljka narasla
                        {
                            // dohvati prihvatljivu poziciju za "sici" s biljke na pod
                            Vector3 climbPosition = sproutGrow.climbPosition;

                            if (climbPosition.y != -1)      // ako imamo validni climb position ide climb
                            {
                                StartCoroutine(climb(collider.transform.position, climbPosition));
                                climbSuccess = true;
                            }
                        }
                        else { Debug.Log("biljka nije narasla"); }
                        break;
                    }
                }
            }
            else { Debug.Log("iznad glave"); }
            if (!climbSuccess)
            {
                // NOTE: tu ide reject animacija
            }
            climbSuccess = false;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.position = transform.position + new Vector3(0, 1.25f, 0);
        }
    }

    // Funkcija koja ostvaruje penjanje
    IEnumerator climb(Vector3 sproutPosition, Vector3 climbPosition)
    {
        isClimbing = true;

        // NOTE: animacija koja priblizi biljci? / lerp za pocetak penjanja
        Vector3 sproutDirection = (sproutPosition - transform.position).normalized;
        sproutDirection.y = 0;
        Quaternion rotateTo = Quaternion.LookRotation(sproutDirection);
        Quaternion initialRotation = transform.rotation;
        for (int i = 0; i < 60; i++)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, rotateTo, (float)(i + 1) / 60);     // slerp prema biljci (rotacija)
            yield return null;
        }
        //int rotationDirection;        // neki attempt za rb move, al why fix it
        //if ((rotateTo.y - transform.rotation.y + 360) % 360 < 180)
        //{
        //    rotationDirection = 1;
        //}
        //else { rotationDirection = -1; }
        //Quaternion deltaRotation = Quaternion.Euler(rotationDirection * new Vector3(0, 90, 0) * Time.fixedDeltaTime);
        //while (Math.Abs(transform.rotation.y - rotateTo.y) > 0.5)
        //{
        //    rb.MoveRotation(transform.rotation * deltaRotation);
        //    yield return null;
        //}

        // play climb animation
        //animator.SetTrigger("isClimbing");
        Vector3 initialPosition = transform.position;
        for (int i = 0; i < 240; i++)
        {
            transform.position = Vector3.Lerp(initialPosition, initialPosition + new Vector3(0, 2, 0), (float)(i + 1) / 240);     // lerp prema gore
            yield return null;
        }

        // play dismount animation
        initialPosition = transform.position;
        for (int i = 0; i < 120; i++)
        {
            transform.position = Vector3.Lerp(initialPosition, climbPosition, (float)(i+1) / 120);     // lerp sunce na poziciju za silazak
            yield return null;
        }

        isClimbing = false;

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

