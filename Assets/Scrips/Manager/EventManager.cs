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

    public event Action<bool> onStartPlayerAttackAnim;
    public event Action onEndPlayerAttackAnim;

    public event Action<Vector3> onLeapPortalPlayer;
    public void StartAttackAnim(bool isAtack)
    {
        onStartPlayerAttackAnim?.Invoke(isAtack);
    }
    public void EndAttackAnim()
    {
        onEndPlayerAttackAnim?.Invoke();
    }
    public void LeapPortalPlayer(Vector3 vec)
    {
        onLeapPortalPlayer?.Invoke(vec);
    }
}
