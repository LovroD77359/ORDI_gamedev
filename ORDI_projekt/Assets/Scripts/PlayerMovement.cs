using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Collider col;
    public LayerMask groundLayer;
    public float speed = 10;
    public float jump = 400;
    public Transform cam;
    public string playerTag;

    [HideInInspector] public bool inputDisabled = false;
    [HideInInspector] public int jumpingForbidden = 0;
    [HideInInspector] public int isGrounded = 0;
    [HideInInspector] public bool isTouchingRock = false;
    [HideInInspector] public int isDebuffed = 0;
    [HideInInspector] public int inMudOrWater = 0;

    private Rigidbody rb;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    public AudioClip walkingSound;        // Zvuk hodanja po tlu
    public AudioClip walkingInWaterSound; // Zvuk hodanja u vodi
    public AudioClip walkingInMudSound;   // Zvuk hodanja u blatu

    private AudioSource audioSource; // AudioSource za reprodukciju zvuka hodanja

    // Kamera smjerovi
    private Vector3 camForward;
    private Vector3 camRight;
    private SproutGrow sproutGrow;

    // Animacije
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        camForward = cam.forward;
        camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;

        if (playerTag == "Player2")
        {
            sproutGrow = GetComponentInChildren<SproutGrow>();
        }

        animator = GetComponent<Animator>();

        // Postavljanje AudioSourcea
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Postavi da se loopa
        audioSource.volume = 0.5f; // Po želji prilagodi glasnoću
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            isGrounded++;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            jumpingForbidden++;
        }
        if (other.CompareTag("MudAndWater"))
        {
            inMudOrWater++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            isGrounded--;
        }
        if (!other.CompareTag("Ground") && !other.CompareTag("MudAndWater") && !other.CompareTag("ScriptCollider"))
        {
            jumpingForbidden--;
        }
        if (other.CompareTag("MudAndWater"))
        {
            inMudOrWater--;
        }

        if (playerTag == "Player2" && isGrounded == 0 && (sproutGrow.isGrown || sproutGrow.isGrowing))
        {
            StartCoroutine(degrow());
        }
    }

    void Update()
    {
        if (!inputDisabled)
        {
            // Inputs za pokrete
            if (playerTag == "Player1")
            {
                horizontalInput = Input.GetAxis("Horizontal1");
                verticalInput = Input.GetAxis("Vertical1");     
            }
            else if (playerTag == "Player2")
            {
                horizontalInput = Input.GetAxis("Horizontal2");
                verticalInput = Input.GetAxis("Vertical2"); 
            }

            // Kamera smjerovi
            Vector3 forwardRelative = verticalInput * speed * camForward;  
            Vector3 rightRelative = horizontalInput * speed * camRight;

            Vector3 movementDirection = forwardRelative + rightRelative;

            // Kretanje i rotacija
            rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);
            if (movementDirection.magnitude > 0.1f)
            {
                movementDirection = -movementDirection;
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }

            // Zvuk hodanja
            HandleWalkingSound(movementDirection.magnitude > 0.1f);

            // Skakanje
            if (playerTag == "Player1")
            {
                if (Input.GetKeyDown(KeyCode.Return) && isGrounded != 0)
                {
                    if (jumpingForbidden == 0)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                        animator.SetTrigger("isJumping");
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    }
                }
            } 
            else if (playerTag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded != 0)
                {
                    if (sproutGrow.isGrown || sproutGrow.isGrowing)
                    {
                        StartCoroutine(degrow());
                    }

                    if (jumpingForbidden == 0)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                        animator.SetTrigger("isJumping");
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);    
                    }
                }
            }

            // Animacije
            bool isMoving = horizontalInput != 0 || verticalInput != 0;
            if (isMoving)
            {
                if (isTouchingRock)
                {
                    animator.SetBool("isPushing", true);
                }
                else
                {
                    animator.SetBool("isPushing", false);
                    animator.SetTrigger("startRunning");
                }

                if (playerTag == "Player2" && (sproutGrow.isGrown || sproutGrow.isGrowing))
                {
                    StartCoroutine(degrow());
                }
            }
            else
            {
                animator.SetBool("isPushing", false);
                animator.SetTrigger("stopRunning");
            }
        }

        if (isGrounded == 0)
        {
            animator.SetTrigger("startFlying");
        }
        if (isGrounded != 0)
        {
            animator.SetTrigger("stopFlying");
        }
    }

    private void HandleWalkingSound(bool isMoving)
    {
        if (isMoving && !audioSource.isPlaying && isGrounded > 0)
        {
            audioSource.Play();
        }
        else if (!isMoving || isGrounded == 0)
        {
            audioSource.Stop();
        }
    }

    private void HandleSplashingSound (bool isMoving){


        // ako je sunce usporeno, nalazi se u vodi: play voda zvuk
        if(playerTag == "Player1" && isDebuffed == 1 && inMudOrWater == 1){ // && isinmudorwater
            if (isMoving && !audioSource.isPlaying && isGrounded > 0){
                
                PlayWalkingSound(walkingInWaterSound, isMoving);
            }
            else if (!isMoving || isGrounded == 0)
            {
                audioSource.Stop();
            } 
        }

        //ako je sunce neusporeno, nalazi se u blatu: play blato zvuk
        else if (playerTag == "Player1" && isDebuffed == 0 && inMudOrWater == 1){ // && isinmudorwater
            if (isMoving && !audioSource.isPlaying && isGrounded > 0){
                
                PlayWalkingSound(walkingInMudSound, isMoving);            }
            else if (!isMoving || isGrounded == 0)
            {
                audioSource.Stop();
            }
        }

        //ako je biljka usporena, nalazi se u blatu
        else if (playerTag == "Player2" && isDebuffed == 1 && inMudOrWater == 1){ //&& isinmudorwater
            if (isMoving && !audioSource.isPlaying && isGrounded > 0){
                
                PlayWalkingSound(walkingInMudSound, isMoving);
            }
            else if (!isMoving || isGrounded == 0)
            {
                audioSource.Stop();
            }
        }

        //ako ne onda je u vodi
         else if (playerTag == "Player2" && isDebuffed == 0 && inMudOrWater == 1){ //&& isinmudorwater
            if (isMoving && !audioSource.isPlaying && isGrounded > 0){
                
                PlayWalkingSound(walkingInWaterSound, isMoving);
            }
            else if (!isMoving || isGrounded == 0)
            {
                audioSource.Stop();
            }
        }

        
    }

    private void PlayWalkingSound(AudioClip clip, bool isMoving)
    {
        if (isMoving && isGrounded > 0)
        {
            if (audioSource.clip != clip) // Promijeni zvuk ako je drugačiji
            {
                audioSource.Stop();
                audioSource.clip = clip;
                audioSource.Play();
            }
            else if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    public IEnumerator degrow()
    {
        sproutGrow.detectCol.enabled = false;
        inputDisabled = true;
        sproutGrow.isGrown = false;
        sproutGrow.isGrowing = false;
        sproutGrow.stemCol.enabled = false;
        animator.SetTrigger("isUngrowing");
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;
        jumpingForbidden--;
        inputDisabled = false;
    }
}
