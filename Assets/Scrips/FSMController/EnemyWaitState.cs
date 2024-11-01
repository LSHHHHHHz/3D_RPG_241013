using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWaitState : IState<BaseEnemy>
{
    IState<BaseEnemy> afterStartState;
    private float durationTime = 2.0f; 
    private float elapsedTime = 0f;
    public EnemyWaitState(IState<BaseEnemy> state)
    {
        afterStartState = state;
    }

    public void Enter(BaseEnemy actor)
    {
        actor.anim.SetTrigger("DoIdle");
        elapsedTime = 0f; 
    }

    public void Exit(BaseEnemy actor)
    {
    }
    public void Update(BaseEnemy actor)
    {
        elapsedTime += Time.deltaTime; 

        if (elapsedTime >= durationTime)
        {
            actor.fsmController.ChangeState(afterStartState);
        }
    }
}
