using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpStrength = 3.0f;
    public float jumpCooldown = 1.0f;
    public bool canPlayerMove = false;
    

    private Rigidbody playerRigBod;
    private float lastJumpTime;

    void Start()
    {
        playerRigBod = GetComponent<Rigidbody>();
        playerRigBod.freezeRotation = true;
        playerRigBod.useGravity = false;
    }

    void Update()
    {
        if (canPlayerMove)
        {
            playerRigBod.useGravity = true;
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float forwardInput = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = (transform.forward * forwardInput + transform.right * horizontalInput).normalized;
            playerRigBod.linearVelocity = new Vector3(moveDirection.x * speed, playerRigBod.linearVelocity.y, moveDirection.z * speed);

            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime > jumpCooldown)
            {
                playerRigBod.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                lastJumpTime = Time.time;
            }
        }
        else
        {
            playerRigBod.linearVelocity = Vector3.zero;
            playerRigBod.useGravity = false;
        }
    }

    public void playerGameActive()
    {
        canPlayerMove = true;
    }
    public void playerGameNotActive()
    {
        canPlayerMove = false;
    }
    public void playerGameToggle()
    {
        canPlayerMove = !canPlayerMove;
    }
}
