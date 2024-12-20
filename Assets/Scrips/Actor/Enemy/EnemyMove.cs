using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EnemyType
{
    Normar,
    Boss
}
public class EnemyMove : MoveBase
{
    public EnemyType enemyType;
    public bool isOriginPos { get; private set; }
    [SerializeField] float moveArea = 5;
    EnemyDetector enemyDetector;
    protected override void Awake()
    {
        base.Awake();
        enemyDetector = GetComponent<EnemyDetector>();
    }
    private void Update()
    {
        if (enemyType == EnemyType.Normar)
        {
            MoveEnemy();
        }
    }
    public  void MoveEnemy()
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
            if (enemyDetector.isInPossibleAttackRange)
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
        if (!isOriginPos && enemyDetector.isDetectedTarget)
        {
            LookTarget(targetPosition);
        }
        if (!enemyDetector.isDetectedTarget)
        {
            if (Vector3.Distance(transform.position, originPos) > 0.1f)
            {
                LookTarget(originPos);
            }
        }
    }
    public override void MoveEnemy(Vector3 targetPos)
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
            targetPosition = targetPos;
            targetPosition.y = transform.position.y;
            if (enemyDetector.isInPossibleAttackRange)
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
        if (!isOriginPos && enemyDetector.isDetectedTarget) 
        {
            LookTarget(targetPosition);
        }
        if (!enemyDetector.isDetectedTarget)
        {
            if (Vector3.Distance(transform.position, originPos) > 0.1f)
            {
                LookTarget(originPos);
            }
        }
    }
    public void MoveOrigin(Vector3 origin)
    {
        LookTarget(origin);

        if (Vector3.Distance(transform.position, origin) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, origin, moveSpeed * Time.deltaTime *2);
        }
        else
        {
            transform.position = origin; 
        }
    }

}
