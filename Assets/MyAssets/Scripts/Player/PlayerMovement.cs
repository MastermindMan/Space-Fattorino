using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float JUMP_BUFFER_DURATION_SECONDS = 0.15f;

    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GroundCheck groundCheck;
    [Header("Values")]
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float jumpSpeed = 4.0f;

    private bool freezeVertical;
    private Vector3 moveDirection;
    private float jumpBufferTime = Mathf.Infinity;

    //properties
    private bool IsGrounded => groundCheck.IsGrounded;


    void Start()
    {
        SubscribeToEvents();
    }
    private void Update()
    {
        CalculateVertical();
        characterController.Move(Player.CameraControl.FowardPointer.TransformVector(moveDirection) * Time.deltaTime);
        DEBUG_DrawMoveDirectionRay();
        moveDirection.x = moveDirection.z = 0;
    }

    private void MoveController(Vector2 direction)
    {
        if(direction.magnitude > 1)
            direction = direction.normalized;
        direction *= speed;
        moveDirection.x = direction.x;
        moveDirection.z = direction.y;
    }
    private void Jump()
    {
        jumpBufferTime = 0;
    }
    private void CalculateVertical()
    {
        if (freezeVertical)
            return;
        if (IsGrounded)
        {
            if (jumpBufferTime <= JUMP_BUFFER_DURATION_SECONDS)
            {
                moveDirection.y = jumpSpeed;
            }
            else if (moveDirection.y <= 0)
            {
                moveDirection.y = -0.01f;
            }
        }
        else
        {
            jumpBufferTime += Time.deltaTime;
            moveDirection.y += Player.CurrentPlanet.Gravity * Time.deltaTime;
        }
    }

    public void SetVerticalFreezedValue(bool value)
    {
        freezeVertical = value;
        if(freezeVertical)
            moveDirection.y = 0;
    }
    public void SetCharacterControllerEnabledValue(bool value)
    {
        characterController.enabled = value;
    }

    public void SubscribeToEvents()
    {
        EventsManager.MovementDirection.AddListener(MoveController);
        EventsManager.Jump.AddListener(Jump);
    }
    public void UnsubscribeToEvents()
    {
        EventsManager.MovementDirection.RemoveListener(MoveController);
        EventsManager.Jump.RemoveListener(Jump);
    }

    private void DEBUG_DrawMoveDirectionRay()
    {
        Debug.DrawRay(transform.position, moveDirection * 3, Color.blue, Time.deltaTime);
        Debug.DrawRay(transform.position, Player.CameraControl.FowardPointer.TransformVector(moveDirection) * 3, Color.red, Time.deltaTime);
    }

}
