using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
public class ActorMeleeWeapon<T> : MonoBehaviour where T : Actor
{
    public int totalDamage = 10;
    protected Transform swordStartPoint;
    protected Transform swordEndPoint;
    public float attackRange = 0.1f;
    protected IReadOnlyList<T> targets;
    private HashSet<T> attackedTargets = new HashSet<T>();
    private bool isStartAttack = false;
    private bool isInAttackRange = false;
    public bool isPossibleAttackanim = false;
    private void Awake()
    {
        swordStartPoint = transform.Find("StartPos");
        swordEndPoint = transform.Find("EndPos");
    }    
    public virtual void Update()
    {
        if (targets != null)
        {
            DetectTargetInSwordRange();
        }
    }
    protected void DetectTargetInSwordRange()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            T target = targets[i];
            DetectedSwordRange(target);
        }
    }
    private void DetectedSwordRange(T target)
    {
        Vector3 startPoint = swordStartPoint.position;
        Vector3 endPoint = swordEndPoint.position;
        Vector3 midPoint = (startPoint + endPoint) / 2;

        Vector3 targetPos = target.gameObject.transform.position;

        float distanceToStart = Vector3.Distance(targetPos, startPoint);
        float distanceToMid = Vector3.Distance(targetPos, midPoint);
        float distanceToEnd = Vector3.Distance(targetPos, endPoint);

        isInAttackRange = ((distanceToStart <= attackRange || distanceToMid <= attackRange || distanceToEnd <= attackRange)
                          && !attackedTargets.Contains(target) && isStartAttack && isPossibleAttackanim);

        if (isInAttackRange)
        {
            attackedTargets.Add(target);
            SendDamageEvent damageEvent = new SendDamageEvent(totalDamage, this.transform.position);
            target.ReceiveEvent(damageEvent);
        }
    }
    protected void StartAttackAction(bool isAttack)
    {
        isStartAttack = isAttack;
    }
    protected void ResetTarget()
    {
        attackedTargets.Clear();
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
