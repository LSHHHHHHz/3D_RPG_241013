using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuickSkillSlotsUI : BaseInventory
{
    private QuickSkillSlotsData quickSkillSlotsData;

    protected override void Awake()
    {
        base.Awake();
        quickSkillSlotsData = GameData.instance.quickSkillSlotsData;
        SetSlotCount(quickSkillSlotsData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < quickSkillSlotsData.inventoryCount; i++)
        {
            slots[i].currentSlotData = quickSkillSlotsData.slotDatas[i];
            quickSkillSlotsData.slotDatas[i].onDataChanged += slots[i].SetData;
            quickSkillSlotsData.slotDatas[i].SetData(quickSkillSlotsData.slotDatas[i].dataID, quickSkillSlotsData.slotDatas[i].count);
        }
    }
}
