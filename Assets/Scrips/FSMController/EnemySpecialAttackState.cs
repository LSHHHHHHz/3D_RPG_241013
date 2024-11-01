using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialAttackState : IState<BaseEnemy>
{
    bool isSkillAction =false;
    public void Enter(BaseEnemy actor)
    {       
        isSkillAction = false;
        actor.anim.SetTrigger("DoSkill");
    }
    public void Exit(BaseEnemy actor)
    {
        actor.StartAttack(false);
    }
    public void Update(BaseEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);
        if (!isSkillAction)
        {
            isSkillAction = true;
            if (actor.baseEnemyAttack is BossEnemyAttack bossEnemyAttack)
            {
                bossEnemyAttack.ExcuteProjectileAttack(stateInfo);
            }
        }
        else if (stateInfo.IsName("Enemy_Skill") && stateInfo.normalizedTime >= 0.95f)
        {
            actor.fsmController.ChangeState(new EnemyIdleState());
        }
    }
}
