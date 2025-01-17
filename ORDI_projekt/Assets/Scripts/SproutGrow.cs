using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutGrow : MonoBehaviour
{
    public bool isGrown = false;
    [HideInInspector] public Vector3 climbPosition = new Vector3(-1, -1, -1);

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //animator.SetTrigger("isGrowing");

            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad biljke neki objekt koji nije player (jer ce biljka uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f), col => !col.transform.CompareTag("Player")))
            {
                // nadi prihvatljivu poziciju za "sici" s biljke na pod
                climbPosition = findClimbPosition(transform.position);

                if (climbPosition.y != -1)      // ako postoji validni climb position
                {
                    // rotiraj biljku prema zidu na koji ce se sunce penjati
                    Vector3 climbDirection = (climbPosition - transform.position).normalized;
                    climbDirection.y = 0;
                    Quaternion rotateTo = Quaternion.LookRotation(climbDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, 1);     // NOTE: ovaj slerp je trenutno instant, treba ga namistit da bude animacija kao

                    // playaj animaciju rasta biljke NOTE: i naravno dodaj popratne efekte, hitbox ako treba, pa da ude u neko stanje iz kojeg ce ga izbaciti prvi pokret
                    isGrown = true;
                    //ANIMACIJE KOD:
                    animator.SetTrigger("isGrowing");
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = transform.position + new Vector3(0, 1.25f, 0);
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
            Debug.Log(directions[i]);
            if (Array.Exists(Physics.OverlapSphere(position + directions[i], 0.01f), col => col.transform.CompareTag("Ground")) &&    // da postoji blok koji je climbable terrain
                Physics.OverlapSphere(position + directions[i] + new Vector3(0, 1, 0), 0.01f).Length == 0)     // da ne postoji collider iznad koji bi blokirao, moze se stavit not exists not tag decoration npr
            {
                Debug.Log("climbing pozicija na: " + (position + directions[i] + new Vector3(0, 1, 0)).ToString());
                return position + directions[i] + new Vector3(0, 1, 0);     // vracamo odgovarajucu poziciju
            }
        }

        Debug.Log("nema climbing pozicije");
        return new Vector3(-1, -1, -1);     // nema prikladne pozicije za penjanje
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
