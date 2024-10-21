using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MoveBase
{
    public bool isTalk { get; private set; }
    [SerializeField] GameObject wayPointParent;
    List<Vector3> npcMovePos = new List<Vector3>();
    int wayPointIndex = 0;
    NPCDetector npcDetector;
    protected override void Awake()
    {
        base.Awake();
        npcDetector = GetComponent<NPCDetector>();
        SetMovePos();
    }
    private void OnEnable()
    {
        wayPointIndex = 0;
        if (npcMovePos.Count > 0)
        {
            targetPos = npcMovePos[wayPointIndex];
        }
    }
    public override void MoveEnemy()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            wayPointIndex++;
            if (wayPointIndex < npcMovePos.Count)
            {
                targetPos = npcMovePos[wayPointIndex];
            }
            else
            {
                wayPointIndex = 0;
                targetPos = npcMovePos[wayPointIndex];
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (!npcDetector.isDetectedTarget)
        {
            LookTarget(targetPos);
        }
    }
    void SetMovePos()
    {
        for (int i = 0; i < wayPointParent.transform.childCount; i++)
        {
            npcMovePos.Add(wayPointParent.transform.GetChild(i).position);
        }
    }
}
