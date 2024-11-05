using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{
    public FSMController<NormalEnemy> fsmController { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        fsmController = new FSMController<NormalEnemy>(this);
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        fsmController.ChangeState(new EnemyIdleState());
        enemyStatus.onEnemyDeath += NormarEnemyDeath;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        enemyStatus.onEnemyDeath -= NormarEnemyDeath;
    }
    private void Update()
    {
        if (enemyDetector != null)
        {
            fsmController.FSMUpdate();
        }
    }
    private void NormarEnemyDeath()
    {
        fsmController.ChangeState(new EnemyDieState());
    }
}
