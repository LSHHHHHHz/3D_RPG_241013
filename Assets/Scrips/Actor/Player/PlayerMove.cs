using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera_Point;

    public float playerSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public float turnSpeed = 10f;

    private Vector2 moveInput;
    private Vector3 lastMoveDir;
    public float currentSpeed { get; private set; }
    private Vector3 velocity;

    void Update()
    {
        MovePlayer();
        RotPlayer();
        MoveRunPlayer();
        Debug.Log(currentSpeed);
    }

    void MovePlayer()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        Vector3 lookForward = new Vector3(camera_Point.forward.x, 0f, camera_Point.forward.z).normalized;
        Vector3 lookRight = new Vector3(camera_Point.right.x, 0f, camera_Point.right.z).normalized;
        Vector3 playerMoveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        if (isMove)
        {
            lastMoveDir = playerMoveDir;
            currentSpeed = Mathf.Lerp(currentSpeed, playerSpeed, acceleration * Time.deltaTime);
        }
        else if (!isMove && lastMoveDir != Vector3.zero)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * deceleration);
            playerMoveDir = lastMoveDir;
        }

        controller.Move((currentSpeed * Time.deltaTime) * playerMoveDir + new Vector3(0, velocity.y, 0) * Time.deltaTime);

        if (currentSpeed < 0.1f)
        {
            lastMoveDir = Vector3.zero;
        }
    }
    void MoveRunPlayer()
    {
        if(Input.GetButton("Run"))
        {
            currentSpeed = 10;
        }
        if(Input.GetButtonUp("Run"))
        {
            currentSpeed = 5;
        }
    }
    void RotPlayer()
    {
        Vector3 playerDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        if (playerDir != Vector3.zero)
        {
            Vector3 forward = camera_Point.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = camera_Point.right;
            right.y = 0;
            right.Normalize();

            Vector3 playerMoveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(playerMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
