using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemInventoryPopupUI : BaseInventory
{
    private ItemInventoryData itemInventoryData;

    protected override void Awake()
    {
        base.Awake();
        itemInventoryData = GameData.instance.itemInventoryData;
        SetSlotCount(itemInventoryData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < itemInventoryData.inventoryCount; i++)
        {
            itemInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            slots[i].currentSlotData = itemInventoryData.slotDatas[i];
        }
    }
}
