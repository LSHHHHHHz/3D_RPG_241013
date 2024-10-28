using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    private static EventManager _instance;
    public static EventManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }
            return _instance;
        }
    }
    public event Action<Vector3> onLeapPortalPlayer;

    public event Action<float> onZommIn;
    public event Action<float> onZommOut;
    public event Action<float, float> onStartCameraShake;
    public event Action onEndCameraShake;

    public event Action<int> onTalkNPC;
    public event Action<int> onEndTalkNPC;
    public void LeapPortalPlayer(Vector3 vec)
    {
        onLeapPortalPlayer?.Invoke(vec);
    }
    public void ZommIn(float duration)
    {
        onZommIn.Invoke(duration);
    }
    public void ZoomOut(float duration)
    {
        onZommOut.Invoke(duration);
    }
    public void StartCameraShake(float duration, float matitude)
    {
        onStartCameraShake?.Invoke(duration, matitude);
    }
    public void EndCameraShake()
    {
        onEndCameraShake?.Invoke();
    }
    public void StartTalkNPC(int npcBranch)
    {
        onTalkNPC?.Invoke(npcBranch);
    }
    public void EndTalkNPC(int npcBranch)
    {
        onEndTalkNPC?.Invoke(npcBranch);
    }
}
