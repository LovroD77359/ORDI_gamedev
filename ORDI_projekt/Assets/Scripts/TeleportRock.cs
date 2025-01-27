//collider na vodi ili blatu mora imati istrigger ukljucen
//mali loophole je da likovi koji mogu skakati u mediju mogu double jumpati jer valjda detektiraju
//medij kroz koji prolaze kao platformu/solidan objekt na kojem mogu skakati

using UnityEngine;

public class TeleportRock : MonoBehaviour
{
    public float targetX;
    public float targetY;
    public float targetZ;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Rock"))
        {
            collider.transform.position = new Vector3(targetX, targetY, targetZ);
            rb = collider.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}