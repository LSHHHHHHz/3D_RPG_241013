using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDropItem : DropItem
{
    int coinAmount;
    public override void AcquiredByPlayer()
    {
        player.currency.GetCoin(coinAmount);
    }

    public override void SetData(string id)
    {
        MonsterEntity monster = GameManager.instance.gameDB.GetEnemyProfileDB(id);
        coinAmount = monster.rewardCoin;
    }
}