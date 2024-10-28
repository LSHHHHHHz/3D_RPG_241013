using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class BaseEnemy : Actor
{
    [SerializeField] string enemyID;
    public EnemyDetector enemyDetector;
    EnemyMove enemyMove;
    [SerializeField] float startAttackTime;
    public FSMController<BaseEnemy> fsmController { get; private set; }
    public EnemyStatus enemyStatus { get; private set; }
    Generator generator;
    public Animator anim { get; private set; }
    public Action<bool> onStartEnemyAttackAnim;
    public Action onEndEnemyAttackAnim;
    public Action onDeathEnemy;
    private void Awake()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        enemyMove = GetComponent<EnemyMove>();
        fsmController = new FSMController<BaseEnemy>(this);
        anim = GetComponentInChildren<Animator>();
        generator = GetComponentInChildren<Generator>();
    }
    private void OnEnable()
    {
        enemyStatus = new EnemyStatus(10);
        enemyMove.PossibleMove();
        enemyStatus.onEnemyDeath += EnemyDeath;
        ActorManager<BaseEnemy>.instnace.RegisterActor(this);
        fsmController.ChangeState(new EnemyIdleState());
    }
    private void OnDisable()
    {
        onDeathEnemy?.Invoke();
        enemyStatus.onEnemyDeath -= EnemyDeath;
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
            enemyStatus.ReduceHP(damageEvent.damage);
            generator.GenerateText(damageEvent.damage.ToString(),transform.position);
            generator.GenerateGetHitPrefab(transform.position, damageEvent.subjectPos);
        }
    }
    public float GetStartAttackTime()
    {
       return startAttackTime;
    }
    public string GetEnemyID()
    {
        return enemyID;
    }
    public void PerformDeathActions()
    {
        ActorManager<BaseEnemy>.instnace.UnregisterActor(this);
        gameObject.SetActive(false);
        Vector3 dropPosition = transform.position;
        GameManager.instance.dropItemManager.DropItem(dropPosition, enemyID);
    }
    private void EnemyDeath()
    {
        fsmController.ChangeState(new EnemyDieState());
    }
}
