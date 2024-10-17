using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuickPortionSlotsUI : BaseInventory
{
    private QuickPortionSlotsData quickPortionSlotsData;

    protected override void Awake()
    {
        base.Awake();
        quickPortionSlotsData = GameData.instance.quickPortionSlotsData;
        SetSlotCount(quickPortionSlotsData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < quickPortionSlotsData.inventoryCount; i++)
        {
            slots[i].currentSlotData = quickPortionSlotsData.slotDatas[i];
            quickPortionSlotsData.slotDatas[i].onDataChanged += slots[i].SetData;
            quickPortionSlotsData.slotDatas[i].SetData(quickPortionSlotsData.slotDatas[i].dataID, quickPortionSlotsData.slotDatas[i].count);
        }
    }
}
