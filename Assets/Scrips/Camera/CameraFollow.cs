using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    bool keyQ;
    float currentAngle = 0f;
    public float cameraSpeed = 10;
    public float rotationSpeed = 5.0f;
    public float currentPitch = 0f;
    public float pitchRange = 30f;
    private float elapsedTime = 0f;
    private void Start()
    {
        offset = new Vector3(0, 3, -5);
    }
    private void OnEnable()
    {
        elapsedTime = 0;
    }
    void LateUpdate()
    {
        elapsedTime += Time.deltaTime;
       // keyQ = Input.GetButton("CameraRotation");

        if (keyQ)
        {
            currentAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            currentPitch += Input.GetAxis("Mouse Y") * rotationSpeed;
            currentPitch = Mathf.Clamp(currentPitch, -pitchRange, pitchRange);
        }
        Quaternion horizontalRotation = Quaternion.Euler(0, currentAngle, 0);
        Quaternion verticalRotation = Quaternion.Euler(currentPitch, 0, 0);
        Quaternion combinedRotation = horizontalRotation * verticalRotation;
        Vector3 rotatedOffset = combinedRotation * offset;
        if (elapsedTime <= 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + rotatedOffset, cameraSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = playerTransform.position + rotatedOffset;
        }
        transform.LookAt(playerTransform);
    }
}

