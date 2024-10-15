using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : IState<NPC>
{
    public void Enter(NPC actor)
    {
        actor.anim.SetBool("IsIdle", true);
    }

    public void Exit(NPC actor)
    {
        actor.anim.SetBool("IsIdle", false);
    }

    public void Update(NPC actor)
    {
        if(!actor.npcDetector.isDetectedPlayer)
        {
            actor.fsmController.ChangeState(new NPCWalkState());
        }
    }
}
