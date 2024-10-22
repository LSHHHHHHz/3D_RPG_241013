using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerPhysicsController : MonoBehaviour
{
    public Vector3 velocity;
    float gravity = -9.81f;
    [SerializeField] CharacterController characterController;
    private void FixedUpdate()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }
}
