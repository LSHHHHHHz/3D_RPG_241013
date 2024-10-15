using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotateSpeed = 720.0f;
    [SerializeField] float moveArea = 5;
    float originMoveSpeed;

    Vector3 targetPos;
    Vector3 originPos;
    Quaternion targetRot;
    EnemyDetector enemyDetector;
    private void Awake()
    {
        originMoveSpeed = moveSpeed;
        originPos = transform.position;
        enemyDetector = GetComponent<EnemyDetector>();
    }
    private void Update()
    {
        MoveEnemy();
    }
    void MoveEnemy()
    {
        Vector3 targetPosition;

        if (enemyDetector.detectedTarget != null)
        {
            targetPosition = enemyDetector.detectedTarget.transform.position;
        }
        else
        {
            targetPosition = originPos;
        }
        targetPosition.y = transform.position.y;
        if (Vector3.Distance(originPos, targetPosition) < enemyDetector.detectedRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if(Vector3.Distance(originPos, targetPosition) == enemyDetector.possibleAttackRange)
        {
            Debug.Log("АјАн");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, moveSpeed * Time.deltaTime);
        }
        if (!enemyDetector.isDetectedPlayer)
        {
            LookTarget(targetPosition);  
        }
    }
    public void LookTarget(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;  
        targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = originMoveSpeed;
    }
    public void StopMove()
    {
        moveSpeed = 0;
    }
}
