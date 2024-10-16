using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActorMeleeWeapon<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Transform swordStartPoint;
    protected Transform swordEndPoint;
    public float attackRange = 0.1f;
    protected IReadOnlyList<T> targets;
    private HashSet<T> attackedTarget = new HashSet<T>();
    private bool isStartAttack = false;
    private void Awake()
    {
        swordStartPoint = transform.Find("StartPos");
        swordEndPoint = transform.Find("EndPos");
    }    
    public virtual void Update()
    {
        if (targets != null)
        {
            DetectEnemiesInSwordRange();
        }
    }
    protected void DetectEnemiesInSwordRange()
    {
        foreach (T target in targets)
        {
            Vector3 targetPos = target.transform.position;
            float distanceToLine = DistanceFromPointToLine(targetPos, swordStartPoint.position, swordEndPoint.position);

            if (distanceToLine <= attackRange && !attackedTarget.Contains(target) && isStartAttack)
            {
                attackedTarget.Add(target);
                Debug.Log("공격 성공!");
                Debug.Log("TargetName : " + target.name);
            }
        }
    }
    protected void StartAttackAction(bool isAttack)
    {
        isStartAttack = isAttack;
    }
    protected void ResetTarget()
    {
        attackedTarget.Clear();
    }
    float DistanceFromPointToLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 line = lineEnd - lineStart;
        Vector3 toPoint = point - lineStart;

        float t = Mathf.Clamp01(Vector3.Dot(toPoint, line) / line.sqrMagnitude);
        Vector3 closestPoint = lineStart + t * line;

        return Vector3.Distance(point, closestPoint);
    }

    private void OnDrawGizmos()
    {
        if (swordStartPoint != null && swordEndPoint != null)
        {
            float swordLength = Vector3.Distance(swordStartPoint.position, swordEndPoint.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(swordStartPoint.position, swordEndPoint.position);
        }
    }
}
