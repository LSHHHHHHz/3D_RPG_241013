using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    NPCDetector npc;
    public bool isTalk { get; private set; }
    [SerializeField] GameObject wayPointParent; 
    [SerializeField] float moveSpeed = 5.0f;    
    [SerializeField] float rotateSpeed = 720.0f;
    float originMoveSpeed;

    List<Vector3> npcMovePos = new List<Vector3>();
    Vector3 targetPos;                        
    int wayPointIndex = 0;                     
    Quaternion targetRot;                   

    private void Awake()
    {
        npc = GetComponent<NPCDetector>();
        SetMovePos();  
        originMoveSpeed = moveSpeed;
    }
    private void OnEnable()
    {
        wayPointIndex = 0;
        if (npcMovePos.Count > 0)
        {
            targetPos = npcMovePos[wayPointIndex];
        }
    }

    private void Update()
    {
        MoveNPC();
    }
    void SetMovePos()
    {
        for (int i = 0; i < wayPointParent.transform.childCount; i++)
        {
            npcMovePos.Add(wayPointParent.transform.GetChild(i).position);
        }
    }
    void MoveNPC()
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
        if (!npc.isDetectedPlayer)
        {
            LookTarget(targetPos);
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
