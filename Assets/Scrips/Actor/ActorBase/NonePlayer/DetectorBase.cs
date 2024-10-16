using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorBase : MonoBehaviour
{
    public bool isDetectedPlayer { get; set; }
    public Player detectedTarget { get; set; }
    public float detectedRange = 5;
    public float possibleAttackRange = 1;
    protected IReadOnlyList<Actor> actors;
    protected MoveBase moveBase;

    protected virtual void Awake()
    {
        moveBase = GetComponent<MoveBase>();
    }

    protected virtual void Update()
    {
        DetectPlayer(actors); 
    }
    protected abstract void DetectPlayer(IReadOnlyList<Actor> actors);
}
