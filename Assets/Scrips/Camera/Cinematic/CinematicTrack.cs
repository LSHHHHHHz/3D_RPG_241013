using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CinematicTrack : MonoBehaviour
{
    [SerializeField] CinemachineBlendListCamera cinemachineBlendListCamera;
    [SerializeField] CinemachineVirtualCamera[] cinemachineVirtualCameras;
    [SerializeField] CinematicActor[] cinematicActors;
    int currentCameraIndex = 0;
    private void Start()
    {
        StartCoroutine(ActionCinematicTrack());
    }
    private IEnumerator ActionCinematicTrack()
    {
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
                if (i < cinematicActors.Length && cinematicActors[j].GetActorPerformingIndex() == i)
                {
                    cinematicActors[i].PerformAction();
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
}
