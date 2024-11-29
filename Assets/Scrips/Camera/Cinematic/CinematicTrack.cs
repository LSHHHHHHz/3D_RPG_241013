using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New CinematicTrackData", menuName = "Cinematic/CinematicTrackData")]
public class CinematicTrackData : ScriptableObject
{
    public float movePos;
    public string anim;
}
public class CinematicTrack : MonoBehaviour
{
    [SerializeField] CinemachineBlendListCamera cinemachineBlendListCamera;
    [SerializeField] CinemachineVirtualCamera[] cinemachineVirtualCameras;
    [SerializeField] CinematicActor[] cinematicActors;
    [SerializeField] Camera mainCamera;
    int currentCameraIndex = 0;
    private void OnEnable()
    {
        StartCoroutine(ActionCinematicTrack());
        mainCamera.GetComponent<CameraFollow>().enabled = false;
    }
    private void OnDisable()
    {
        mainCamera.GetComponent<CameraFollow>().enabled = true;
    }
    private IEnumerator ActionCinematicTrack()
    {
        cinemachineBlendListCamera.enabled = false;
        yield return StartCoroutine(MoveMainCameraToVirtualCamera(cinemachineVirtualCameras[0].gameObject, 1f));
        cinemachineBlendListCamera.enabled = true;
        for (int i = 0; i < cinemachineBlendListCamera.m_Instructions.Length; i++)
        {
            if (cinemachineBlendListCamera.m_Instructions.Length - 1 == i) 
            {
                var ins = cinemachineBlendListCamera.m_Instructions[i];
                float totalWaitTime = ins.m_Blend.m_Time + ins.m_Hold;

                yield return new WaitForSeconds(totalWaitTime - 0.1f);
                cinemachineVirtualCameras[i].LookAt = GameManager.instance.player.transform;
            }
            if (cinemachineBlendListCamera.m_Instructions.Length-2 ==i)
            {
                SetFirstAndLastPos(GameManager.instance.player.transform.position + new Vector3(0,3,-5));
            }
            var instruction = cinemachineBlendListCamera.m_Instructions[i];

            yield return new WaitForSeconds(instruction.m_Blend.m_Time + instruction.m_Hold);

            for (int j = 0; j < cinematicActors.Length; j++)
            {
                if ( cinematicActors[j].GetActorPerformingIndex() == i)
                {
                    cinematicActors[j].PerformAction();
                }
            }
            currentCameraIndex = i + 1;
        }
        gameObject.SetActive(false);
    }
    public void SetFirstAndLastPos(Vector3 pos)
    {
        cinemachineVirtualCameras[cinemachineVirtualCameras.Length - 1].transform.position = pos;
    }
    private IEnumerator MoveMainCameraToVirtualCamera(GameObject obj, float duration)
    {
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        Vector3 targetPosition = obj.transform.position;
        Quaternion targetRotation = obj.transform.rotation;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
    }
}
