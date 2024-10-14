using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController
{
    private BaseEnemy baseEnemy;
    IState currentState;
    public FSMController(BaseEnemy enemy)
    {
        this.baseEnemy = enemy;
    }
    public void ChangeState(IState newState)
    {
        currentState?.Exit(baseEnemy);
        currentState = newState;
        currentState.Enter(baseEnemy);
    }
    public void FSMUpdate()
    {
        currentState.Update(baseEnemy);
    }
}
