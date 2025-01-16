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
    private bool shouldMoveToTarget = false;  // Status kretanja prema ciljnoj poziciji
    private bool shouldReturnToInitial = false; // Status kretanja prema početnoj poziciji

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
        if (shouldMoveToTarget)
        {
            

            // Pomicanje prema ciljnoj poziciji
            rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * vectorToTarget);

            // Provjerava je li platforma dovoljno blizu cilja
            if (Vector3.Distance(platform.transform.position, targetPosition) < correctionOffset)
            {
                platform.transform.position = targetPosition;
                rb.velocity = Vector3.zero;
                shouldMoveToTarget = false;  // Platforma je stigla na cilj
            }
        }
        // Ako je platforma u povratku prema početnoj poziciji
        else if (shouldReturnToInitial)
        {
            // Pomicanje prema početnoj poziciji
            rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * -vectorToTarget);

            // Provjerava je li platforma dovoljno blizu početne pozicije
            if (Vector3.Distance(platform.transform.position, initialPosition) < correctionOffset)
            {
                platform.transform.position = initialPosition;
                rb.velocity = Vector3.zero;
                shouldReturnToInitial = false;  // Povratak završen
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kada bilo koji objekt dotakne gumb, postavljamo da platforma krene prema cilju
        shouldMoveToTarget = true;
        shouldReturnToInitial = false;
    }

    private void OnTriggerExit(Collider other)
    {
        // Kada objekt napusti gumb, postavljamo da platforma krene prema početnoj poziciji
        shouldMoveToTarget = false;
        shouldReturnToInitial = true;
    }
}
