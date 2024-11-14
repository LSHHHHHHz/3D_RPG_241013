using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorBase : MonoBehaviour
{
    public bool isDetectedTarget { get; set; }
    public Player detectedTarget { get; set; }
    [SerializeField] protected float detectedRange = 4;
    [SerializeField] protected float activeDetectedRange = 2;
    protected IReadOnlyList<Actor> actors;
    protected MoveBase moveBase;

    protected virtual void Awake()
    {
        moveBase = GetComponent<MoveBase>();
    }
  
    protected abstract void DetectPlayer(IReadOnlyList<Actor> actors);
    public float GetDetectRange()
    {
        return detectedRange;
    }
    public float GetActiveDetectRange()
    {
        return activeDetectedRange;
    }
}
