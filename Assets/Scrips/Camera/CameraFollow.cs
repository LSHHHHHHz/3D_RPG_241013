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

    Camera mainCamera;
    public float zoomDuration = 1f;
    Vector3 originOffset = new Vector3(0, 3, -5);
    Vector3 zoomOffset = new Vector3(0, 2, -3);

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    private void Start()
    {
        offset = originOffset;
    }
    private void OnEnable()
    {
        elapsedTime = 0;
    }
    public IEnumerator ZoomIn()
    {
        Debug.Log("¡‹¿Œ Ω√¿€");
        float elapsedTime = 0f;
        Vector3 startOffset = offset;
        while (elapsedTime < zoomDuration)
        {
            offset = Vector3.Lerp(startOffset, zoomOffset, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        offset = zoomOffset;
    }
    public IEnumerator ZoomOut()
    {
        Debug.Log("¡‹æ∆øÙ Ω√¿€");
        float elapsedTime = 0f;
        Vector3 startOffset = offset; 
        while (elapsedTime < zoomDuration)
        {
            offset = Vector3.Lerp(startOffset, originOffset, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        offset = originOffset; 
    }
    void LateUpdate()
    {
        elapsedTime += Time.deltaTime;
        keyQ = Input.GetButton("CameraRotation");

        if (keyQ)
        {
            currentAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            currentPitch += Input.GetAxis("Mouse Y") * rotationSpeed;
            currentPitch = Mathf.Clamp(currentPitch, -pitchRange, pitchRange);
        }
        Quaternion horizontalRotation = Quaternion.Euler(0, currentAngle, 0);
        Quaternion verticalRotation = Quaternion.Euler(-currentPitch, 0, 0);
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

