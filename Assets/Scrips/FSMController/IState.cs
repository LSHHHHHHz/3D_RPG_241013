using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(BaseEnemy actor);
    void Update(BaseEnemy actor);
    void Exit(BaseEnemy actor);
}
