using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyApproachPlayerState : IState<BaseEnemy>
{
    Transform player;
    float approachSpeed = 10f;

    public void Enter(BaseEnemy actor)
    {
        player = GameObject.FindObjectOfType<Player>().transform;
        actor.anim.SetBool("IsRun", true);
    }

    public void Update(BaseEnemy actor)
    {
        if ((actor.IsPlayerDetected() || actor.enemyMove.CheckEnemyMove()) && !actor.IsOriginPos())
        {
            if (player != null)
            {
                actor.transform.position = Vector3.MoveTowards(actor.transform.position, player.position, approachSpeed * Time.deltaTime);

                if (Vector3.Distance(actor.transform.position, player.position) < 2.0f)
                {
                    actor.fsmController.ChangeState(new EnemyAttackState());
                }
            }
        }
        if (!actor.IsPlayerDetected() && !actor.IsOriginPos())
        {
            actor.fsmController.ChangeState(new EnemyWalkState());
        }
    }
    public void Exit(BaseEnemy actor)
    {
        actor.anim.SetBool("IsRun", false);
    }
}
