using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerAnimation anim;
    [SerializeField] PlayerPhysicsController physicsController;
    public Transform camera_Point;

    public float playerSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public float turnSpeed = 10f;
    public float jumpPower = 5;
    public bool doJump {  get; private set; }

    private Vector2 moveInput;
    private Vector3 lastMoveDir;
    public float currentSpeed { get; private set; }

    bool isLeap = false;
    private void OnEnable()
    {
        EventManager.instance.onLeapPortalPlayer += LeapPlayer;
    }
    private void OnDisable()
    {
        EventManager.instance.onLeapPortalPlayer -= LeapPlayer;
    }
    private void LateUpdate()
    {
        if (!isLeap)
        {
            RotPlayer();
            MoveRunPlayer();
            MovePlayer();
            JumpPlayer();
        }
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
        controller.Move((currentSpeed * Time.deltaTime) * playerMoveDir + new Vector3(0, physicsController.velocity.y, 0) * Time.deltaTime);
        if (currentSpeed < 0.1f)
        {
            lastMoveDir = Vector3.zero;
        }
    }
    void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !doJump)  
        {
            physicsController.velocity = new Vector3(physicsController.velocity.x, jumpPower, physicsController.velocity.z);
            doJump = true;
        }
        if (!controller.isGrounded)
        {
            physicsController.velocity += Physics.gravity * Time.deltaTime;  
        }
        if (controller.isGrounded)
        {
            doJump = false;
        }
    }
    void LeapPlayer(Vector3 vec)
    {
        StopAllCoroutines();
        isLeap = true;
        StartCoroutine(LeapPortalPlayer(vec));
    }
    IEnumerator LeapPortalPlayer(Vector3 vec)
    {
        anim.DoLeapJump();
        EventManager.instance.ZommIn(2);
        yield return new WaitForSeconds(3.67f);
        while (Vector3.Distance(transform.position, vec) > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, vec, Time.deltaTime * 5);
            yield return null;
        }
        EventManager.instance.ZoomOut(0.5f);
        isLeap = false;
    }
    void MoveRunPlayer()
    {
        if (Input.GetButton("Run"))
        {
            currentSpeed = 10;
        }
        if (Input.GetButtonUp("Run"))
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
            controller.gameObject.transform.rotation = Quaternion.Slerp(controller.gameObject.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
