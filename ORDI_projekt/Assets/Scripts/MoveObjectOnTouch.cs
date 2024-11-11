using UnityEngine;

public class MovePlatformOnButtonPress : MonoBehaviour
{
    public GameObject platform;          // Referenca na platformu koja se pomiče
    public float targetX;                // Ciljna pozicija X osi
    public float targetY;                // Ciljna pozicija Y osi
    public float targetZ;                // Ciljna pozicija Z osi
    public float moveSpeed = 2.0f;       // Brzina pomicanja platforme

    private Vector3 initialPosition;     // Početna pozicija platforme
    private Vector3 targetPosition;      // Ciljna pozicija platforme
    private bool shouldMoveToTarget = false;  // Status kretanja prema ciljnoj poziciji
    private bool shouldReturnToInitial = false; // Status kretanja prema početnoj poziciji

    private void Start()
    {
        // Spremamo početnu poziciju platforme
        initialPosition = platform.transform.position;

        // Definiramo ciljnu poziciju na temelju unesenih koordinata
        targetPosition = new Vector3(targetX, targetY, targetZ);
    }

    private void Update()
    {
        // Ako je platforma u pokretu prema cilju
        if (shouldMoveToTarget)
        {
            // Pomicanje prema ciljnoj poziciji
            platform.transform.position = Vector3.Lerp(platform.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Provjerava je li platforma dovoljno blizu cilja
            if (Vector3.Distance(platform.transform.position, targetPosition) < 0.01f)
            {
                platform.transform.position = targetPosition;
                shouldMoveToTarget = false;  // Platforma je stigla na cilj
            }
        }
        // Ako je platforma u povratku prema početnoj poziciji
        else if (shouldReturnToInitial)
        {
            // Pomicanje prema početnoj poziciji
            platform.transform.position = Vector3.Lerp(platform.transform.position, initialPosition, Time.deltaTime * moveSpeed);

            // Provjerava je li platforma dovoljno blizu početne pozicije
            if (Vector3.Distance(platform.transform.position, initialPosition) < 0.01f)
            {
                platform.transform.position = initialPosition;
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
