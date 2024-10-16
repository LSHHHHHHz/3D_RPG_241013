using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5.0f;
    [SerializeField] protected float rotateSpeed = 720.0f;

    protected float originMoveSpeed;
    protected Vector3 targetPos;
    protected Vector3 originPos;
    protected Quaternion targetRot;

    protected virtual void Awake()
    {
        originMoveSpeed = moveSpeed;
        originPos = transform.position;
    }
    private void Update()
    {
        MoveEnemy();
    }
    public abstract void MoveEnemy();
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
