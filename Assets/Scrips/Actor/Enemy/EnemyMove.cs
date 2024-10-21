using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveBase
{
    public bool isOriginPos { get; private set; }
    [SerializeField] float moveArea = 5;
    EnemyDetector enemyDetector;
    protected override void Awake()
    {
        base.Awake();
        enemyDetector = GetComponent<EnemyDetector>();
    }

    public override void MoveEnemy()
    {
        if (!isPossibleMove)
        {
            return;
        }
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            isOriginPos = false;
        }
        else
        {
            isOriginPos = true;
        }
        Vector3 targetPosition = Vector3.zero;
        if (enemyDetector.isDetectedTarget)
        {
            targetPosition = enemyDetector.detectedTarget.transform.position;
            targetPosition.y = transform.position.y;
            if (enemyDetector.isPossibleAttack)
            {
                StopMove();
            }
            else
            {
                ResetMoveSpeed();
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, moveSpeed * Time.deltaTime);
            ResetMoveSpeed();
        }
        if (!isOriginPos)
        {
            LookTarget(targetPosition);
        }
        if (!enemyDetector.isDetectedTarget)
        {
            if (Vector3.Distance(transform.position, originPos) > 0.1f)
            {
                LookTarget(targetPosition);
            }
        }
    }
}
