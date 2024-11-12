using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Actor
{
    [SerializeField] private string enemyID;
    public EnemyDetector enemyDetector;
    public EnemyMove enemyMove { get; private set; }
    [SerializeField] private float startAttackTime;
    public EnemyStatus enemyStatus { get; private set; }
    private MonsterEntity monsterEntity;
    private Generator generator;
    public Animator anim { get; private set; }
    public Action<bool> onStartEnemyAttackAnim;
    public Action onEndEnemyAttackAnim;
    public Action onDeathEnemy;

    public int currentHPTest;
    private void Update()
    {
        currentHPTest = enemyStatus.enemyCurrentHP;
    }
    protected virtual void Awake()
    {
        enemyDetector = GetComponent<EnemyDetector>();
        enemyMove = GetComponent<EnemyMove>();
        anim = GetComponentInChildren<Animator>();
        generator = GetComponentInChildren<Generator>();
    }
    protected virtual void OnEnable()
    {
        monsterEntity = GameManager.instance.gameDB.GetEnemyProfileDB(enemyID);
        enemyStatus = new EnemyStatus(monsterEntity.monsterMaxHP);
        enemyMove.PossibleMove();
        enemyStatus.onEnemyDeath += EnemyDeath;
        ActorManager<BaseEnemy>.instnace.RegisterActor(this);
    }
    protected virtual void OnDisable()
    {
        onDeathEnemy?.Invoke();
        enemyStatus.onEnemyDeath -= EnemyDeath;
    }
    public bool IsPlayerDetected()
    {
        return enemyDetector.detectedTarget != null;
    }
    public bool IsPossibleAttack()
    {
        return enemyDetector.isInPossibleAttackRange;
    }
    public bool IsOriginPos()
    {
        return enemyMove.isOriginPos;
    }
    public void StartAttack(bool isAttack)
    {
        if (isAttack)
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
        if (ievent is SendDamageEvent damageEvent)
        {
            enemyStatus.ReduceHP(damageEvent.damage);
            generator.GenerateText(damageEvent.damage.ToString(), transform.position);
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
    public float GetEnemyMaxHP()
    {
        return enemyStatus.enemyMaxHP;
    }
    public float GetEnemyCurrentHp()
    {
        return enemyStatus.enemyCurrentHP;
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
        onDeathEnemy?.Invoke();
    }
}
