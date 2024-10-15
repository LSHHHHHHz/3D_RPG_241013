using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class NPCDetector : MonoBehaviour
{
    public bool isDetectedPlayer { get; private set; }
    [SerializeField] float detectedRange =5;
    [SerializeField] float possibleTalkRange = 1;
    IReadOnlyList<Player> player;
    NPCMove npcMove;
    private void Awake()
    {
        npcMove = GetComponent<NPCMove>();
    }
    private void Update()
    {
        player = ActorManager<Player>.instnace.GetActors();
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        foreach (Player p in player)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= detectedRange)
            {
                npcMove.StopMove();
                npcMove.LookTarget(p.transform.position);
                isDetectedPlayer = true;
            }
            else
            {
                isDetectedPlayer = false;
                npcMove.ResetMoveSpeed();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, possibleTalkRange);
    }
}