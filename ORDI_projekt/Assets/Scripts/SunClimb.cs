using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunClimb : MonoBehaviour
{
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 centeredPosition = centerPosition(transform.position);      // ako ne postoji dva bloka iznad sunca neki objekt koji nije player (jer ce sunce uhvatit svoj collider), dakle gore je prazno
            if (!Array.Exists(Physics.OverlapCapsule(centeredPosition + new Vector3(0, 1, 0), centeredPosition + new Vector3(0, 2, 0), 0.4f), col => !col.transform.CompareTag("Player")))
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);        // trazimo collidere oko sunca NOTE: igrat se s ovim radijusom
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.CompareTag("Player") && collider.transform != transform)     // ako je objekt player i nije sunce (sebe detektira) NOTE: dodati uvjet da je biljka narasla
                    {
                        // nadi prihvatljivu poziciju za "sici" s biljke na pod
                        Vector3 climbPosition = findClimbPosition(collider.transform.position);     // NOTE: possibly mozemo ovo sve olaksati i climb position utvrditi kod biljke, pa ga nekako passati tu

                        if (climbPosition.y != -1)      // ako imamo validni climb position ide climb, NOTE: dodati neki bool koji kaze da se dogodio climb, da se izbjegne reject animacija
                        {
                            climb(collider.transform.position, climbPosition);
                        }
                        break;
                    }
                }
            }
            else { Debug.Log("iznad glave"); }
        }
    }

    // Funkcija koja ostvaruje penjanje
    void climb(Vector3 sproutPosition, Vector3 climbPosition)
    {
        //mozda prvo lerp malo blize biljci ako je moguce bit daleko NOTE: je, a treba i rotirat
        
        // play climb animation
        animator.SetTrigger("isClimbing");
        transform.position += new Vector3(0, 2, 0);     // NOTE: ovdje ide lerp prema gore

        // lerp sunce na poziciju za silazak

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
                    new(x_dif/Mathf.Abs(x_dif), 1, 0), new(0, 1, z_dif/Mathf.Abs(z_dif)),       // punimo polje s vektorima (1, 1.5, 0), (-1, 1.5, 0), (0, 1.5, 1) i (0, 1.5, -1), sortirano po udaljenosti
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

        // provjeravamo sad u tim smjerovima jesu li ostvareni uvjeti da se tamo moze popeti
        for (int i = 0; i < directions.Length; i++)
        {
            if (Array.Exists(Physics.OverlapSphere(position + directions[i], 0.01f), col => col.transform.CompareTag("ClimbableTerrain")) &&    // da postoji blok koji je climbable terrain NOTE: treba dodat tagove
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

