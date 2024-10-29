using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemInventoryPopupUI : BaseInventory
{
    private ItemInventoryData itemInventoryData;

    private void OnEnable()
    {
        EventManager.instance.PossibleAttack(false);
    }
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
            slots[i].currentSlotData = itemInventoryData.slotDatas[i];
            itemInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            itemInventoryData.slotDatas[i].SetData(itemInventoryData.slotDatas[i].dataID, itemInventoryData.slotDatas[i].count);
        }
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
        EventManager.instance.PossibleAttack(true);
    }
}
