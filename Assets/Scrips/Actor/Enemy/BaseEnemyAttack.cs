using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public abstract class BaseEnemyAttack : MonoBehaviour
{
    protected BaseEnemy enemy;
    protected float isStartAttackTime;
    private void Awake()
    {
        enemy = GetComponent<BaseEnemy>();
        isStartAttackTime = enemy.GetStartAttackTime();
    }
    public abstract void AttackAction();
}