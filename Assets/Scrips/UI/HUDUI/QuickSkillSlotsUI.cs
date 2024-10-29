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
            if (slots[i] is QuickSkillSlotUI quickSkillSlot) 
            {
                quickSkillSlot.currentSlotData = quickSkillSlotsData.slotDatas[i];
                quickSkillSlotsData.slotDatas[i].onDataChanged += quickSkillSlot.SetData;
                quickSkillSlotsData.slotDatas[i].SetData(quickSkillSlotsData.slotDatas[i].dataID, quickSkillSlotsData.slotDatas[i].count);
            }
        }
    }
    public void ActivateSlotSkill(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            if (slots[slotIndex] is QuickSkillSlotUI quickSkillSlot) 
            {
                quickSkillSlot.ActivateSlotSkill();
            }
        }
    }
}
