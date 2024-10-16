using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetector : DetectorBase
{
    [SerializeField] private float possibleTalkRange = 1;

    protected override void Awake()
    {
        base.Awake();
        moveBase = GetComponent<NPCMove>();
    }
    protected override void Update()
    {
        actors = ActorManager<Player>.instnace.GetActors();
        base.Update();
    }
    protected override void DetectPlayer(IReadOnlyList<Actor> players)
    {
        foreach (Player p in actors)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= detectedRange)
            {
                moveBase.StopMove();
                moveBase.LookTarget(p.transform.position);
                isDetectedPlayer = true;
            }
            else
            {
                isDetectedPlayer = false;
                moveBase.ResetMoveSpeed();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, possibleTalkRange);
    }
}
