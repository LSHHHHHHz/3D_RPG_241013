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
        EventManager.instance.onZommIn += StartZoomIn;
        EventManager.instance.onZommOut += StartZoomOut;
    }
    private void OnDisable()
    {
        EventManager.instance.onZommIn -= StartZoomIn;
        EventManager.instance.onZommOut -= StartZoomOut;
    }
    public void StartZoomIn(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomIn(duration));
    }
    public void StartZoomOut(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut(duration));
    }
    public IEnumerator ZoomIn(float duration)
    {
        offset = originOffset;
        float elapsedTime = 0f;
        Vector3 startOffset = offset;
        while (elapsedTime < duration)
        {
            offset = Vector3.Lerp(startOffset, zoomOffset, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        offset = zoomOffset;
    }
    public IEnumerator ZoomOut(float duration)
    {
        float elapsedTime = 0f;
        Vector3 startOffset = offset;
        while (elapsedTime < duration)
        {
            offset = Vector3.Lerp(startOffset, originOffset, elapsedTime / duration);
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
        if(Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.E))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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

