using UnityEngine;

public class MoveTargetOnTouch : MonoBehaviour
{
    public GameObject targetObject; // Referenca na platformu
    private Animator targetAnimator; // Animator za kontrolu animacija
    private bool isMoving = false; // Status kretanja
    private Vector3 targetPosition; // Pozicija prema kojoj platforma ide

    private void Start()
    {
        targetAnimator = targetObject.GetComponent<Animator>(); // Dohvatimo Animator komponentu
        targetPosition = targetObject.transform.position; // Početna pozicija platforme
    }

    private void Update()
    {
        // Provjeri je li platforma u pokretu
        if (isMoving && !targetAnimator.GetCurrentAnimatorStateInfo(0).IsName("MoveAnimation"))
        {
            // Ako je MoveAnimation završila, postavimo novu poziciju
            targetObject.transform.position = targetPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
            // Pokreće animaciju pomicanja
            targetAnimator.SetBool("IsPressed", true); // Pokrećemo animaciju kretanja
            isMoving = true; // Platforma se pomiče
            targetPosition = targetObject.transform.position;
        
    }

    private void OnTriggerExit(Collider other)
    {
        
            // Platforma se zaustavlja na trenutnoj poziciji
            isMoving = false; // Prestanak pomicanja

            // Počinjemo s animacijom povratka
            targetAnimator.SetBool("IsPressed", false);
            // Postavi ciljnu poziciju na trenutnu poziciju platforme
            targetPosition = targetObject.transform.position;
        
    }
}
