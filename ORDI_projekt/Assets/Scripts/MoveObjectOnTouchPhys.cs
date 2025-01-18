using UnityEngine;
using UnityEngine.Windows;

public class MovePlatformOnButtonPressPhys : MonoBehaviour
{
    public GameObject platform;          // Referenca na platformu koja se pomiče
    public float targetX;                // Ciljna pozicija X osi
    public float targetY;                // Ciljna pozicija Y osi
    public float targetZ;                // Ciljna pozicija Z osi
    public float moveSpeed = 2.0f;       // Brzina pomicanja platforme

    private float correctionOffset;
    private Rigidbody rb;
    private Vector3 initialPosition;     // Početna pozicija platforme
    private Vector3 targetPosition;      // Ciljna pozicija platforme
    private Vector3 vectorToTarget;
    private int buttonPressCount = 0;  // Status kretanja prema ciljnoj poziciji
    
    private void Start()
    {
        correctionOffset = moveSpeed * 0.03f;

        // Spremamo početnu poziciju platforme
        initialPosition = platform.transform.position;

        // Definiramo ciljnu poziciju na temelju unesenih koordinata
        targetPosition = new Vector3(targetX, targetY, targetZ);

        vectorToTarget = (targetPosition - initialPosition).normalized;

        rb = platform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Ako je platforma u pokretu prema cilju
        if (buttonPressCount > 0)
        {
            // Provjerava je li platforma dovoljno blizu cilja
            if (Vector3.Distance(platform.transform.position, targetPosition) < correctionOffset)
            {
                platform.transform.position = targetPosition;
                rb.velocity = Vector3.zero;
            }
            else
            {
                // Pomicanje prema ciljnoj poziciji
                rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * vectorToTarget);
            }
        }
        // Ako je platforma u povratku prema početnoj poziciji
        else if (buttonPressCount == 0)
        {
            // Provjerava je li platforma dovoljno blizu početne pozicije
            if (Vector3.Distance(platform.transform.position, initialPosition) < correctionOffset)
            {
                platform.transform.position = initialPosition;
                rb.velocity = Vector3.zero;
            }
            else
            {
                // Pomicanje prema početnoj poziciji
                rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * -vectorToTarget);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kada bilo koji objekt dotakne gumb, povecavamo button press count
        buttonPressCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        // Kada objekt napusti gumb, smanjujemo button press count
        buttonPressCount--;
    }
}
