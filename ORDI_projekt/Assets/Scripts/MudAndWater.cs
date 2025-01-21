//collider na vodi ili blatu mora imati istrigger ukljucen
//mali loophole je da likovi koji mogu skakati u mediju mogu double jumpati jer valjda detektiraju
//medij kroz koji prolaze kao platformu/solidan objekt na kojem mogu skakati

using UnityEngine;

public class DiscriminativeMediums : MonoBehaviour
{
    public GameObject affectedPlayer;       // lik koji propadne
    private PlayerMovement playerMovement;
    private Collider objectCollider;
    private float ogJump;

    private void Start()
    {      
        objectCollider = GetComponent<Collider>();
        playerMovement = affectedPlayer.GetComponent<PlayerMovement>();
        ogJump = playerMovement.jump;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            playerMovement.speed *= 0.5f;
            playerMovement.jump = 0;
        }
        //else{
        //    Physics.IgnoreCollision(collider, objectCollider, false);
        //}
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            playerMovement.speed *= 2;
            playerMovement.jump = ogJump;
        }
        //else{
        //    Physics.IgnoreCollision(collider, objectCollider, false);
        //}
        }
}