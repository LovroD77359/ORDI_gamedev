using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    public GameObject walkingParticleEmitter;
    public GameObject normalLiquidParticleEmitter;
    public GameObject debuffLiquidParticleEmitter;
    public float YOffset;

    private PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementScript.isMoving && movementScript.isGrounded > 0)
        {
            walkingParticleEmitter.SetActive(!(movementScript.inMudOrWater > 0));
            normalLiquidParticleEmitter.SetActive(movementScript.inMudOrWater > 0 && !(movementScript.isDebuffed > 0));
            debuffLiquidParticleEmitter.SetActive(movementScript.inMudOrWater > 0 && movementScript.isDebuffed > 0);
            normalLiquidParticleEmitter.transform.position = new Vector3(
                    normalLiquidParticleEmitter.transform.position.x,
                    (int)(transform.position.y + YOffset),
                    normalLiquidParticleEmitter.transform.position.z
                );
            debuffLiquidParticleEmitter.transform.position = new Vector3(
                    debuffLiquidParticleEmitter.transform.position.x,
                    (int)(transform.position.y + YOffset),
                    debuffLiquidParticleEmitter.transform.position.z
                );
        }
        else
        {
            walkingParticleEmitter.SetActive(false);
            normalLiquidParticleEmitter.SetActive(false);
            debuffLiquidParticleEmitter.SetActive(false);
            normalLiquidParticleEmitter.transform.localPosition = Vector3.zero;
            debuffLiquidParticleEmitter.transform.localPosition = Vector3.zero;
        }
    }
}
