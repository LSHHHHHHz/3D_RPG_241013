using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EquipmentInventoryUI : BaseInventory
{
    [SerializeField] int equipInventoryNum;
    private ItemEquipInventoryData itemEquipInventoryData;

    protected override void Awake()
    {
        base.Awake();
        if (equipInventoryNum == 1)
        {
            itemEquipInventoryData = GameData.instance.firstItemEquipInventoryData;
        }
        else if (equipInventoryNum == 2)
        {
            itemEquipInventoryData = GameData.instance.secondItemEquipInventoryData;
        }     
        SetSlots();
        SetSlotData();
    }
    private void OnEnable()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].currentSlotData.onEquipItem += GameManager.instance.equipmentManager.EquipItem;
        }
    }
    private void OnDisable()
    {
        for(int i =0; i < slots.Count; i++)
        {
            slots[i].currentSlotData.onEquipItem -= GameManager.instance.equipmentManager.EquipItem;
        }
    }
    void SetSlots()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BaseSlotUI childSlot = transform.GetChild(i).GetComponent<BaseSlotUI>();
            if (childSlot != null)
            {
                slots.Add(childSlot);
            }
        }
    }
    void SetSlotData()
    {
        for (int i = 0; i < itemEquipInventoryData.inventoryCount; i++)
        {
            slots[i].currentSlotData = itemEquipInventoryData.slotDatas[i];
            itemEquipInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            itemEquipInventoryData.slotDatas[i].SetData(itemEquipInventoryData.slotDatas[i].dataID, itemEquipInventoryData.slotDatas[i].count);
        }
    }
}
