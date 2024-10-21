using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorBase : MonoBehaviour
{
    public bool isDetectedTarget { get; set; }
    public Player detectedTarget { get; set; }
    [SerializeField] protected float detectedRange = 3;   
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
