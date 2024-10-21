using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerPhysicsController : MonoBehaviour
{
    public Vector3 velocity;
    float gravity = -9.81f;
    private void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
    }
}
