using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : DetectorBase
{
    Vector3 originPos;
    protected override void Awake()
    {
        base.Awake();
        originPos = transform.position;
    }
    protected override void Update()
    {
        actors = ActorManager<Player>.instnace.GetActors();
        base.Update();
    }
    protected override void DetectPlayer(IReadOnlyList<Actor> players)
    {
        foreach (Player actor in actors)
        {
            float distance = Vector3.Distance(originPos, actor.transform.position);
            if (distance <= detectedRange)
            {
                moveBase.LookTarget(actor.transform.position);
                isDetectedPlayer = true;
                detectedTarget = actor;
                break;
            }
            else
            {
                if (isDetectedPlayer)
                {
                    isDetectedPlayer = false;
                    detectedTarget = null;
                    moveBase.ResetMoveSpeed();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPos, detectedRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, possibleAttackRange);
    }
}
