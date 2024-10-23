using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortionDropItem : DropItem
{
    string itemID;
    public override void AcquiredByPlayer()
    {
        for (int i = 0; i < GameData.instance.itemInventoryData.slotDatas.Count; i++)
        {
            if (string.IsNullOrEmpty(GameData.instance.itemInventoryData.slotDatas[i].dataID))
            {
                GameData.instance.itemInventoryData.slotDatas[i].SetData(itemID, 1);
                break;
            }
        }
    }
    public override void SetData(string id)
    {
        MonsterEntity monster = GameManager.instance.gameDB.GetEnemyProfileDB(id);
        itemID = monster.rewardPortion;     
    }
}