using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originPos;
    CameraFollow cameraFollow;
    private void Awake()
    {
        originPos = transform.position;
        cameraFollow= GetComponent<CameraFollow>();
    }
    private void OnEnable()
    {
        EventManager.instance.onStartCameraShake += StartCameraShake;
        EventManager.instance.onEndCameraShake += EndCameraShake;
    }
    private void OnDisable()
    {
        EventManager.instance.onStartCameraShake -= StartCameraShake;
        EventManager.instance.onEndCameraShake -= EndCameraShake;
    }
    public void StartCameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraFollow.offset;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            cameraFollow.offset = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;  

            yield return null;
        }
        cameraFollow.offset = originalPos;
    }
    void EndCameraShake()
    {
        StopAllCoroutines();
        transform.position = originPos;
    }
}

