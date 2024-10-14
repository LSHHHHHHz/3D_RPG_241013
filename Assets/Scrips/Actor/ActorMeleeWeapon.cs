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
            Vector3 enemyPosition = target.transform.position;
            float distanceToLine = DistanceFromPointToLine(enemyPosition, swordStartPoint.position, swordEndPoint.position);

            if (distanceToLine <= attackRange && !attackedTarget.Contains(target))
            {
                attackedTarget.Add(target);
                Debug.Log("공격 성공!");
                StartCoroutine(ResetAttacks(target));
            }
        }
    }
    IEnumerator ResetAttacks(T target)
    {
        float elapsedTime = 0;
        while (elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        attackedTarget.Remove(target);
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
