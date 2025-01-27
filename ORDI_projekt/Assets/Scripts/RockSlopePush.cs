//collider na vodi ili blatu mora imati istrigger ukljucen
//mali loophole je da likovi koji mogu skakati u mediju mogu double jumpati jer valjda detektiraju
//medij kroz koji prolaze kao platformu/solidan objekt na kojem mogu skakati

using UnityEngine;

public class RockSlopePush : MonoBehaviour
{
    private Rigidbody rb;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Rock"))
        {
            rb = collider.GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Rock"))
        {
            rb = collider.GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
}