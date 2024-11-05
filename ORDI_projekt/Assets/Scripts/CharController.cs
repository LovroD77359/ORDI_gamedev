using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharController : MonoBehaviour
{
    
    private CharacterController characterController;
    private Character character;
    private Vector3 movingDirection;
    private Transform cam;
    private Vector3 camForward;
    private bool jump;
   
    
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        characterController = tempPlayer.GetComponent<CharacterController>();  
        character = tempPlayer.GetComponent<Character>();
        
        
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            }

    }

    // Update is called once per frame
    void Update()
    {
        float inX = Input.GetAxis("Horizontal");
        float inZ = Input.GetAxis("Vertical");
        jump = CrossPlatformInputManager.GetButton("Jump");
        
        
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            movingDirection = (inZ * camForward + inX * cam.right).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            movingDirection = (inZ * Vector3.forward + inX * Vector3.right).normalized;
        }

        
        
    }

    private void FixedUpdate()
    {
        character.Move(movingDirection, jump);
        jump = false;
    }
}
