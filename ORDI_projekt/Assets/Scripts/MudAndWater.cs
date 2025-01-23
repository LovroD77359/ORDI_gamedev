//collider na vodi ili blatu mora imati istrigger ukljucen
//mali loophole je da likovi koji mogu skakati u mediju mogu double jumpati jer valjda detektiraju
//medij kroz koji prolaze kao platformu/solidan objekt na kojem mogu skakati

using UnityEngine;

public class DiscriminativeMediums : MonoBehaviour
{
    public GameObject affectedPlayer;       // lik koji propadne
    private PlayerMovement movementScript;
    private float ogJump;

    private void Start()
    {
        movementScript = affectedPlayer.GetComponent<PlayerMovement>();
        ogJump = movementScript.jump;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            movementScript.speed *= 0.5f;
            movementScript.jump = 0;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            movementScript.speed *= 2;
            movementScript.jump = ogJump;
        }
    }
}