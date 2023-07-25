using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public void Awake()
    {
        instance = this;
    }

    // script handles movement of the player
    [SerializeField] Vector3 moveH, moveV, move;
    public Transform playerHead;
    [SerializeField] CharacterController characterController; // our character controller
    [SerializeField] float moveSpeed, gravity, jumpVelocity, playerJumpVelocity; // set in editor for controlling
    float gravityValue, verticalVelocity; // hidden because is calculated
    bool landed;
    [SerializeField] Transform respawnPoint;

    [Header("Camera")]
    [SerializeField] float aimSensitivity;
    [SerializeField] float minYAngle, maxYAngle;
    [SerializeField] float currentSensitivity, yRotate, xRotate, maxRot;

    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // declare our motion
        float pAxisV = Input.GetAxisRaw("Vertical");
        float pAxisH = Input.GetAxisRaw("Horizontal");
        moveV = playerHead.forward * pAxisV;
        moveH = playerHead.right * pAxisH;

        RaycastHit groundedHit;
        Physics.Raycast(transform.position, Vector3.down, out groundedHit, characterController.height/2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        // movement application
        // jump calculations
        gravityValue = gravity;

        // if we are NOT grounded
        if (!characterController.isGrounded)
        {
            playerJumpVelocity += gravityValue * Time.fixedDeltaTime;
            landed = false;
        } // if we are gounded
        else if (characterController.isGrounded)
        {
            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerJumpVelocity = Mathf.Sqrt(jumpVelocity * -3.0f * gravity);
            }
            else if (!landed)
            {
                playerJumpVelocity = 0f;
                landed = true;
            }
        }

        if (landed) playerJumpVelocity = 0; 

        verticalVelocity = playerJumpVelocity;
        move = new Vector3((moveH.x + moveV.x), verticalVelocity / moveSpeed, (moveH.z + moveV.z));
        // adjust it to our slope
        move = AdjustVelocityToSlope(move);
        // apply the movement
        characterController.Move(move * Time.fixedDeltaTime * moveSpeed);

        // our camera control
        currentSensitivity = aimSensitivity;
        // run math to rotate the head of the player as we move the mouse
        float addY = Input.GetAxis("Mouse Y") * -currentSensitivity * Time.fixedDeltaTime;
        if (addY > maxRot) yRotate += maxRot;
        else if (addY < -maxRot) yRotate -= maxRot;
        else if (addY > -maxRot && addY < maxRot) yRotate += addY;

        // clamp the rotation so we don't go around ourselves
        yRotate = Mathf.Clamp(yRotate, minYAngle, maxYAngle);
        // calculate our X rotation
        float addX = Input.GetAxis("Mouse X") * currentSensitivity * Time.fixedDeltaTime;
        // make sure we dont surpass the highest rot
        if (addX > maxRot) xRotate += maxRot;
        else if (addX < -maxRot) xRotate -= maxRot;
        else if (addX > -maxRot && addX < maxRot) xRotate += addX;

        // add in our rotate mods if we have any
        float finalxRotate = xRotate;
        float finalyRotate = yRotate;

        Mathf.SmoothStep(xRotate, finalxRotate, 5 * Time.fixedDeltaTime);
        Mathf.SmoothStep(yRotate, finalyRotate, 5 * Time.fixedDeltaTime);

        // apply it to our head
        playerHead.rotation = Quaternion.Euler(new Vector3(finalyRotate, finalxRotate, 0f));
    }

    // adjust our velocity to the slope we're on
    RaycastHit adjusterHit;
    int ignoreLayerMask;
    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out adjusterHit, 2f, ignoreLayerMask, QueryTriggerInteraction.Ignore))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, adjusterHit.normal);
            var adjustedVelocity = slopeRotation * velocity; // this will align the velocity with the surface

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }
}