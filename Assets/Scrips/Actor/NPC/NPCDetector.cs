using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetector : DetectorBase
{
    public float possibleTalkRange = 1;
    public bool isPossibleTalk { get; private set; } = false;
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
                isDetectedTarget = true;
                if (Vector3.Distance(transform.position, p.transform.position) <= possibleTalkRange)
                {
                    isPossibleTalk = true;
                }
                else
                {
                    isPossibleTalk=   false;
                }
            }
            else
            {
                isDetectedTarget = false;
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
