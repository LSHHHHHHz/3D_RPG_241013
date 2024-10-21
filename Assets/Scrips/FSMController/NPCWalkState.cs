using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkState : IState<NPC>
{
    public void Enter(NPC actor)
    {
    }

    public void Exit(NPC actor)
    {
    }

    public void Update(NPC actor)
    {
        if (actor.npcDetector.isDetectedTarget)
        {
            actor.fsmController.ChangeState(new NPCIdleState());
        }
    }
}
