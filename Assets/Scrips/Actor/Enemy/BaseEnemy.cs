using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class BaseEnemy : Actor
{
    public EnemyDetector enemyDetector;
    EnemyMove enemyMove;
    [SerializeField] float startAttackTime;
    public FSMController<BaseEnemy> fsmController { get; private set; }
    public Animator anim { get; private set; }
    public Action<bool> onStartEnemyAttackAnim;
    public Action onEndEnemyAttackAnim;
    private void Awake()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        enemyMove = GetComponent<EnemyMove>();
        fsmController = new FSMController<BaseEnemy>(this);
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        ActorManager<BaseEnemy>.instnace.RegisterActor(this);
        fsmController.ChangeState(new EnemyIdleState());
    }
    private void OnDisable()
    {
        ActorManager<BaseEnemy>.instnace.UnregisterActor(this);
    }
    private void Update()
    {
        if (enemyDetector != null)
        {
            fsmController.FSMUpdate();
        }
    }
    public bool IsPlayerDetected()
    {
        return enemyDetector.detectedTarget != null;
    }
    public bool IsPossibleAttack()
    {
        return enemyDetector.isPossibleAttack;
    }
    public bool IsOriginPos()
    {
        return enemyMove.isOriginPos;
    }
    public void StartAttack(bool isAttack)
    {
        if(isAttack)
        {
            enemyMove.StopMove();
        }
        else
        {
            enemyMove.ResetMoveSpeed();
        }
    }
    public override void ReceiveEvent(IEvent ievent)
    {
        if(ievent is SendDamageEvent damageEvent)
        {

        }
    }
    public float GetStartAttackTime()
    {
       return startAttackTime;
    }
}
