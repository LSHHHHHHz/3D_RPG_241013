using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class BaseEnemy : Actor
{
    public FSMController<BaseEnemy> fsmController { get; private set; }
    public Animator anim { get; private set; }
    public Action<bool> onStartEnemyAttackAnim;
    public Action onEndPlayerAttackAnim;
    private void Awake()
    {
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
        fsmController.FSMUpdate();
    }
}
