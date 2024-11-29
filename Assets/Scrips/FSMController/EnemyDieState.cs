using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyDieState : IState<NormalEnemy>
{
    bool isDie;
    public void Enter(NormalEnemy actor)
    {
        isDie = false;
        ActorManager<NormalEnemy>.instnace.UnregisterActor(actor);
        actor.anim.SetTrigger("DoDeath");
    }

    public void Exit(NormalEnemy actor)
    {
    }
    public void Update(NormalEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Death") && stateInfo.normalizedTime >= 0.95f && !isDie)
        {
            isDie = true;
            actor.PerformDeathActions();
        }
    }
}
