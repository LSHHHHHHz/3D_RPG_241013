using UnityEngine;

public abstract class CinematicActor : MonoBehaviour
{
    [SerializeField]int actorPerformingIndex;
    public abstract void PerformAction();
    public int GetActorPerformingIndex()
    {
        return actorPerformingIndex;
    }
}
