using UnityEngine;

public class DiscriminativeMediums : MonoBehaviour
{
    public GameObject affectedPlayer;       //lik koji propadne
    private Collider objectCollider;        //medij kroz koji lik propadne       
    private Collider affectedPlayerCollider;     // collider lika
    
    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        affectedPlayerCollider = affectedPlayer.GetComponent<Collider>();
        Physics.IgnoreCollision(affectedPlayerCollider, objectCollider, true);
    }
}
