using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutGrow : MonoBehaviour
{
    public bool isGrown = false;
    public bool isGrowing = false;
    public Collider stemCol;
    public Collider detectCol;
    [HideInInspector] public Vector3 climbPosition = new Vector3(-1, -1, -1);

    private Animator animator;
    private Rigidbody rb;
    private PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        detectCol = GetComponent<Collider>();
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && movementScript.isGrounded != 0)
        {
            movementScript.inputDisabled = true;
            rb.velocity = Vector3.zero;

            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad biljke neki objekt koji nije player (jer ce biljka uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f),
                col => (!col.transform.CompareTag("Player") && !col.transform.CompareTag("Decoration") && !col.transform.CompareTag("GroundCollider")
                        && !col.transform.CompareTag("ScriptCollider"))) && movementScript.jumpingForbidden == 0 && movementScript.inMudOrWater == 0)
            {
                // nadi prihvatljivu poziciju za "sici" s biljke na pod
                climbPosition = findClimbPosition(transform.position);

                if (climbPosition.y != -1)      // ako postoji validni climb position
                {
                    StartCoroutine(grow(climbPosition));
                }
                else
                {
                    StartCoroutine(deny());
                }
            }
            else
            {
                StartCoroutine(deny());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((isGrowing || isGrown) && !other.CompareTag("Player") && !other.CompareTag("GroundCollider"))
        {
            StartCoroutine(movementScript.degrow());
        }
    }

    // Funkcija koja trazi prihvatljivu poziciju za popeti se, odnosno "sici" s biljke
    Vector3 findClimbPosition(Vector3 position)
    {
        // sortiramo smjerove (x+-, z+-) po tome koji je najblizi trenutnoj poziciji biljke
        Vector3[] directions;
        var x_dif = Mathf.Round(position.x) - position.x;   // utvrdujemo je li diferencijal od najblize cijele koord po x/z osi pozitivan ili negativan
        var z_dif = Mathf.Round(position.z) - position.z;

        if (Mathf.Abs(x_dif) < Mathf.Abs(z_dif))    // utvrdujemo koji je manji diferencijal
        {
            directions = new Vector3[]
            {
                    new(x_dif/Mathf.Abs(x_dif), 1, 0), new(0, 1, z_dif/Mathf.Abs(z_dif)),       // punimo polje s vektorima (1, 1, 0), (-1, 1, 0), (0, 1, 1) i (0, 1, -1), sortirano po udaljenosti
                    new(0, 1, -z_dif/Mathf.Abs(z_dif)), new(-x_dif/Mathf.Abs(x_dif), 1, 0)
            };
        }
        else
        {
            directions = new Vector3[]
            {
                    new(0, 1, z_dif/Mathf.Abs(z_dif)), new(x_dif/Mathf.Abs(x_dif), 1, 0),
                    new(-x_dif/Mathf.Abs(x_dif), 1, 0), new(0, 1, -z_dif/Mathf.Abs(z_dif))
            };
        }

        position = centerPosition(position);

        // provjeravamo sad u tim smjerovima jesu li ostvareni uvjeti da se tamo moze sunce popeti
        for (int i = 0; i < directions.Length; i++)
        {
            if (Array.Exists(Physics.OverlapSphere(position + directions[i], 0.01f), col => col.transform.CompareTag("Ground")) &&    // da postoji blok koji je climbable terrain
                !Array.Exists(Physics.OverlapSphere(position + directions[i] + new Vector3(0, 1, 0), 0.01f), col => col.transform.CompareTag("Ground")))     // da ne postoji blok iznad koji bi blokirao
            {
                Debug.Log("climbing pozicija na: " + (position + directions[i] + new Vector3(0, 1, 0)).ToString());
                return position + directions[i] + new Vector3(0, 1.38f, 0);     // vracamo odgovarajucu poziciju
            }
        }

        Debug.Log("nema climbing pozicije");
        return new Vector3(-1, -1, -1);     // nema prikladne pozicije za penjanje
    }

    IEnumerator grow(Vector3 climbPosition)
    {
        movementScript.jumpingForbidden++;

        // rotiraj biljku prema zidu na koji ce se sunce penjati
        Vector3 climbDirection = (climbPosition - transform.position).normalized;
        climbDirection.y = 0;
        Quaternion rotateTo = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(climbDirection).eulerAngles.y - 180, 0));
        Quaternion initialRotation = transform.parent.rotation;
        float startTime = Time.time;
        float timeDif;
        while (Quaternion.Angle(transform.parent.rotation, rotateTo) > 5)
        {
            timeDif = Time.time - startTime;
            transform.parent.rotation = Quaternion.Slerp(initialRotation, rotateTo, timeDif * 2);     // slerp prema zidu (rotacija)
            yield return null;
        }
        transform.parent.rotation = rotateTo;

        // playaj animaciju rasta biljke
        animator.SetTrigger("isGrowing");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        detectCol.enabled = true;
        yield return new WaitForSeconds(0.5f);
        movementScript.inputDisabled = false;
        isGrowing = true;
        yield return new WaitForSeconds(1f);
        if (isGrowing)
        {
            isGrown = true;
            isGrowing = false;
            stemCol.enabled = true;
        }
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
