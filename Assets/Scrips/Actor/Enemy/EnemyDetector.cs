using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : DetectorBase
{
    public float possibleAttackRange { get; private set; } = 1;
    public bool isInPossibleAttackRange { get; private set; }
    Vector3 originPos;

    protected override void Awake()
    {
        base.Awake();
        originPos = transform.position;
    }
    void Update()
    {
        actors = ActorManager<Player>.instnace.GetActors();
        DetectPlayer(actors);
    }
    protected override void DetectPlayer(IReadOnlyList<Actor> players)
    {
        foreach (Player actor in players)
        {
            Vector3 currentPos = new Vector3(transform.position.x, actor.transform.position.y, transform.position.z);
            float distanceFromOrigin = Vector3.Distance(originPos, actor.transform.position);
            float distanceFromCurrent = Vector3.Distance(currentPos, actor.transform.position);

            if (distanceFromOrigin <= detectedRange)
            {
                isDetectedTarget = true;
                detectedTarget = actor;

                if (distanceFromCurrent <= possibleAttackRange)
                {
                    isInPossibleAttackRange = true;
                }
                else
                {
                    isInPossibleAttackRange = false;
                }
                break;
            }
            else
            {
                if (isDetectedTarget)
                {
                    isDetectedTarget = false;
                    detectedTarget = null;
                    isInPossibleAttackRange = false;
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
