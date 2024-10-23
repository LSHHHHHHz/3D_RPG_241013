using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpDropItem : DropItem
{
    int expAmount;
    public override void AcquiredByPlayer()
    {
        player.status.GetExp(expAmount);
    }

    public override void SetData(string id)
    {
        MonsterEntity monster = GameManager.instance.gameDB.GetEnemyProfileDB(id);
        expAmount = monster.rewardExp;
    }
}