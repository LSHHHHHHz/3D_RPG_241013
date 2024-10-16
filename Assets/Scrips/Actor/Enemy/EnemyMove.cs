using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveBase
{
    [SerializeField] float moveArea = 5;

    protected override void Awake()
    {
        base.Awake();
        detectorBase = GetComponent<EnemyDetector>();
    }

    public override void MoveEnemy()
    {
        Vector3 targetPosition = Vector3.zero;

        if (detectorBase.detectedTarget != null)
        {
            targetPosition = detectorBase.detectedTarget.transform.position;
        }
        else
        {
            targetPosition = originPos;
        }
        targetPosition.y = transform.position.y;

        if (Vector3.Distance(originPos, targetPosition) < detectorBase.detectedRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(originPos, targetPosition) == detectorBase.possibleAttackRange)
        {
            Debug.Log("Attack");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, moveSpeed * Time.deltaTime);
        }

        if (!detectorBase.isDetectedPlayer)
        {
            if(Vector3.Distance(transform.position, originPos)>0.1f)
            {
                LookTarget(targetPosition);
            }
        }
    }
}
