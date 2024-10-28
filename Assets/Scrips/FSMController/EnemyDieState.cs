using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyDieState : IState<BaseEnemy>
{
    bool isDie;
    public void Enter(BaseEnemy actor)
    {
        isDie = false;
        ActorManager<BaseEnemy>.instnace.UnregisterActor(actor);
        actor.anim.SetTrigger("DoDeath");
    }

    public void Exit(BaseEnemy actor)
    {
    }
    public void Update(BaseEnemy actor)
    {
        AnimatorStateInfo stateInfo = actor.anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Death") && stateInfo.normalizedTime >= 0.95f && !isDie)
        {
            Debug.Log("µé¾î¿È");
            isDie = true;
            actor.PerformDeathActions();
        }
    }
}
