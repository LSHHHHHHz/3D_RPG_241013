using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
public class VectorTest : MonoBehaviour
{
    public Transform swordStartPoint;
    public Transform swordEndPoint;
    public Transform targetPoint;
    public float attackRange = 0.5f;
    private bool isInAttackRange = false;
    Vector3 dir;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CheckPointsInRange(targetPoint);
        }
    }
    private void OnDrawGizmos()
    {
        if (swordStartPoint == null || swordEndPoint == null || targetPoint == null)
        {
            return;
        }
        if (isInAttackRange)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(swordStartPoint.position, swordEndPoint.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetPoint.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(swordStartPoint.position, 0.1f);  
        Gizmos.DrawSphere((swordStartPoint.position + swordEndPoint.position) / 2, 0.1f); 
        Gizmos.DrawSphere(swordEndPoint.position, 0.1f);  
    }
    private void CheckPointsInRange(Transform target)
    {
        Vector3 startPoint = swordStartPoint.position;
        Vector3 endPoint = swordEndPoint.position;
        Vector3 midPoint = (startPoint + endPoint) / 2;  

        Vector3 targetPos = target.position;

        float distanceToStart = Vector3.Distance(targetPos, startPoint);
        float distanceToMid = Vector3.Distance(targetPos, midPoint);
        float distanceToEnd = Vector3.Distance(targetPos, endPoint);

        isInAttackRange = (distanceToStart <= attackRange || distanceToMid <= attackRange || distanceToEnd <= attackRange);

        if (isInAttackRange)
        {
            Debug.Log("Target in attack range!");
            Debug.Log("Distance to Start: " + distanceToStart);
            Debug.Log("Distance to Mid: " + distanceToMid);
            Debug.Log("Distance to End: " + distanceToEnd);
        }
    }
}
