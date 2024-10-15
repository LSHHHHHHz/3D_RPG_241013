using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyDetector : MonoBehaviour
{
    public bool isDetectedPlayer { get; private set; }
    public Player detectedTarget {  get; private set; }
    public float detectedRange =5;
    public float possibleAttackRange = 1;
    IReadOnlyList<Player> player;
    EnemyMove enemyMove;
    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
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
                enemyMove.LookTarget(p.transform.position);
                isDetectedPlayer = true;
                detectedTarget = p;
            }
            else
            {
                isDetectedPlayer = false;
                detectedTarget = null;
                enemyMove.ResetMoveSpeed();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, possibleAttackRange);
    }
}