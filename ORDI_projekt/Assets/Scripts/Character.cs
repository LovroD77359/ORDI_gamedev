using UnityEngine;



    public class Character : MonoBehaviour
    {
        [SerializeField] private float movePower = 5;
        [SerializeField] private float jumpPower = 2; // The force added to the ball when it jumps.
        private const float groundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody rigidBody;
        
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        
        
        
        public void Move(Vector3 moveDirection, bool jump)
        {
            // If on the ground and jump is pressed...
            rigidBody.AddForce(moveDirection * movePower);
            
            if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
            {
                // ... add force in upwards direction.
                rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
        
    }
