using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EquipmentInventoryUI : BaseInventory
{
    //스왑 버튼을 누르면 다른 창의 데이터 실행


    [SerializeField] int equipInventoryNum;
    private ItemEquipInventoryData itemEquipInventoryData;

    protected override void Awake()
    {
        base.Awake();
        if (equipInventoryNum == 1)
        {
            itemEquipInventoryData = GameData.instance.firstItemEquipInventoryData;
            //for(int i =0; i< itemEquipInventoryData.slotDatas.Count; i++)
            //{
            //    itemEquipInventoryData.slotDatas[i].onActiveEquipSlot += GameManager.instance.equipmentManager.ActiveEquipPrefab;
            //} gk..
        }
        else if (equipInventoryNum == 2)
        {
            itemEquipInventoryData = GameData.instance.secondItemEquipInventoryData;
        }     
        SetSlots();
        SetSlotData();
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
            slots[i].currentSlotData.onEquipItem += GameManager.instance.equipmentManager.EquipItem;
        }
    }
}
