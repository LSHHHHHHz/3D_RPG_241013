using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveSkillInventoryUI : BaseInventory
{
    ActiveSkillInventoryData activeSkillInventoryData;
    public List<GameDBEntity> activeSkillEntities; //테스트중
    protected override void Awake()
    {
        base.Awake();
        activeSkillEntities = new List<GameDBEntity>();

        for (int i = 0; i < GameManager.instance.gameDB.GameDataEntites.Count; i++)
        {
            if (GameManager.instance.gameDB.GameDataEntites[i].dataType == "active")
            {
                activeSkillEntities.Add(GameManager.instance.gameDB.GameDataEntites[i]);
            }
        }
        activeSkillInventoryData = GameData.instance.activeSkillInventoryData;
        SetSlotCount(activeSkillInventoryData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < activeSkillEntities.Count; i++)
        {
            slots[i].currentSlotData = activeSkillInventoryData.slotDatas[i];
            activeSkillInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            activeSkillInventoryData.slotDatas[i].SetData(activeSkillEntities[i].dataID, 0);
        }
    }
}
