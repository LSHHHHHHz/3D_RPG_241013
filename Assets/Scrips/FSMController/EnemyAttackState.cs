using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<BaseEnemy>
{
    bool endAnim = false;
    public void Enter(BaseEnemy actor)
    {
        actor.anim.SetBool("IsAttack", true);
        actor.StartAttack(true);
    }
    public void Exit(BaseEnemy actor)
    {
        actor.anim.SetBool("IsAttack", false);
        actor.StartAttack(false);
    }
    public void Update(BaseEnemy actor)
    {
        Debug.Log(endAnim);
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack"))
        {
            if (stateInfo.normalizedTime >= 1f)
            {
                if (!endAnim) 
                {
                    endAnim = true;
                    actor.onEndPlayerAttackAnim?.Invoke();
                    actor.onStartEnemyAttackAnim?.Invoke(false);
                }
            }
            else
            {
                endAnim = false; 
                actor.onStartEnemyAttackAnim?.Invoke(true);
            }
        }
        if (!actor.IsPossibleAttack() && endAnim)
        {
            actor.fsmController.ChangeState(new EnemyWalkState());
            endAnim = false; 
        }
    }

}
