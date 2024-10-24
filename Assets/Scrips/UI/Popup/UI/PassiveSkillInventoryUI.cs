using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillInventoryUI : BaseInventory
{
    PassiveSkillInventoryData passiveSkillInventoryData;
    public List<GameDBEntity> passiveSkillEntities;

    protected override void Awake()
    {
        base.Awake();
        passiveSkillEntities = new List<GameDBEntity>();

        for (int i = 0; i < GameManager.instance.gameDB.GameDataEntites.Count; i++)
        {
            if (GameManager.instance.gameDB.GameDataEntites[i].dataType == "passive")
            {
                passiveSkillEntities.Add(GameManager.instance.gameDB.GameDataEntites[i]);
            }
        }
        passiveSkillInventoryData = GameData.instance.passiveSkillInventoryData;
        SetSlotCount(passiveSkillInventoryData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < passiveSkillEntities.Count; i++)
        {
            slots[i].currentSlotData = passiveSkillInventoryData.slotDatas[i];
            passiveSkillInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            passiveSkillInventoryData.slotDatas[i].SetData(passiveSkillEntities[i].dataID, 0);
        }
    }
}
