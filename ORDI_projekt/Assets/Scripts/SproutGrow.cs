using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutGrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ovaj dio skripte sortira smjerove (x+-, z+-) po tome koji je najblizi trenutnoj poziciji
            Vector3[] directions;

            var x_dif = Mathf.Round(transform.position.x) - transform.position.x;   // utvrdujemo je li diferencijal od najblize cijele koord po x/z osi pozitivan ili negativan
            var z_dif = Mathf.Round(transform.position.z) - transform.position.z;

            if (Mathf.Abs(x_dif) < Mathf.Abs(z_dif))    // utvrdujemo koji je manji diferencijal
            {
                directions = new Vector3[]
                {
                    new(x_dif/Mathf.Abs(x_dif), 1, 0), new(0, 1, z_dif/Mathf.Abs(z_dif)),       // punimo polje s vektorima (1, 1, 0), (-1, 1, 0), (0, 1, 1) i (0, 1, -1), sortirano po udaljenosti
                    new(0, 1, -z_dif/Mathf.Abs(z_dif)), new(-x_dif/Mathf.Abs(x_dif), 1, 0)
                };
            } else
            {
                directions = new Vector3[]
                {
                    new(0, 1, z_dif/Mathf.Abs(z_dif)), new(x_dif/Mathf.Abs(x_dif), 1, 0),
                    new(-x_dif/Mathf.Abs(x_dif), 1, 0), new(0, 1, -z_dif/Mathf.Abs(z_dif))
                };
            }


            for (int i = 0; i < directions.Length; i++)
            {
                Debug.Log(directions[i]);
            };

            transform.position = transform.position + new Vector3(0, 1, 0);
        }
    }
}
