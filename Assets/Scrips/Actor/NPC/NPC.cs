using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPC : MonoBehaviour
{
    [SerializeField] float detectedRange =5;
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
                Debug.Log("플레이어가 특정 키 입력 시 팝업 생성");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);
    }
}