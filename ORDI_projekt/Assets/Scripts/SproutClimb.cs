using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SproutClimb : MonoBehaviour
{
    public bool isClimbing = false;

    private bool climbSuccess = false;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad biljke neki objekt koji nije player (jer ce biljka uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f), col => !col.transform.CompareTag("Player")))
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
                            Vector3 climbPosition = centerPosition(collider.transform.position);
                            StartCoroutine(climb(collider.transform.position, climbPosition));      // ide climb
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
                // NOTE: tu ide reject animacija
            }
            climbSuccess = false;
        }
    }

    // Funkcija koja ostvaruje penjanje
    IEnumerator climb(Vector3 hookPlantPosition, Vector3 climbPosition)
    {
        isClimbing = true;

        Vector3 hookPlantDirection = (hookPlantPosition - transform.position).normalized;
        hookPlantDirection.y = 0;
        Quaternion rotateTo = Quaternion.LookRotation(hookPlantDirection);
        Quaternion initialRotation = transform.rotation;
        for (int i = 0; i < 60; i++)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, rotateTo, (float)(i + 1) / 60);     // slerp prema hook plantu (rotacija)
            yield return null;
        }

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

