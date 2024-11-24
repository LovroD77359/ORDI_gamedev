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
    private float ogSpeed;    
    private float newSpeed;   

    private void Start()
    {      
        objectCollider = GetComponent<Collider>();
        playerMovement = affectedPlayer.GetComponent<PlayerMovement>();
        ogJump = playerMovement.getJump();
        ogSpeed = playerMovement.getSpeed();
        newSpeed = 0.5f * ogSpeed;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            playerMovement.setSpeed(newSpeed);
            playerMovement.setJump(0);
        }
        //else{
        //    Physics.IgnoreCollision(collider, objectCollider, false);
        //}
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == affectedPlayer)
        {
            playerMovement.setSpeed(ogSpeed);
            playerMovement.setJump(ogJump);
        }
        //else{
        //    Physics.IgnoreCollision(collider, objectCollider, false);
        //}
    }
}