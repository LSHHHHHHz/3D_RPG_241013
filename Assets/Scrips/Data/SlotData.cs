using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    Equipment,
    Portion,
    SKill
}
[System.Serializable]
public class SlotData
{
    public string dataID;
    public int count;
    public int maxCount = 99;
    public event Action<string, int> onDataChanged;
    public void SetData(string id, int newCount) //여기 아이디가 같은게 있다면 카운트만 늘리면됨
    {
        dataID = id;
        count = newCount;
        onDataChanged?.Invoke(dataID,count);
    }
    public void SwapData(SlotData dropData, SlotData dragData)
    {
        string tempDataId = dragData.dataID;
        int tempDataCount = dragData.count;

        dragData.dataID = dropData.dataID;
        dragData.count = dropData.count;

        dropData.dataID = tempDataId;
        dropData.count = tempDataCount;

        dropData.onDataChanged(dropData.dataID,dropData.count);
        dragData.onDataChanged(dragData.dataID,dragData.count);
    }
    public void MergeData(SlotData dropData, SlotData dragData)
    {
        dropData.count += dragData.count;
        dragData.dataID ="";
        dragData.count = 0;

        dropData.onDataChanged(dropData.dataID, dropData.count);
        dragData.onDataChanged(dragData.dataID, dragData.count);
    }
    public string GetData()
    {
        return dataID;
    }
    public void UsePortion()
    {
        if (count > 0)
        {
            count--;
            onDataChanged?.Invoke(dataID, count);
            if (count == 0)
            {
                dataID = ""; 
                onDataChanged?.Invoke(dataID, count);
            }
        }
    }
}
public class InventoryData
{
    public List<SlotData> slotDatas;
    protected void InitializeSlots(int count)
    {
        slotDatas = new List<SlotData>(count);
        for (int i = 0; i < count; i++)
        {
            slotDatas.Add(new SlotData());
        }
    }
}
[Serializable]
public class ItemInventoryData : InventoryData
{
    public int inventoryCount = 18;
    public ItemInventoryData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}

[Serializable]
public class ItemEquipInventoryData : InventoryData
{
    public int inventoryCount = 2;
    public ItemEquipInventoryData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}
[Serializable]
public class ActiveSkillInventoryData : InventoryData
{
    public int inventoryCount = 6;
    public ActiveSkillInventoryData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}
[Serializable]
public class PassiveSkillInventoryData : InventoryData
{
    public int inventoryCount = 6;
    public PassiveSkillInventoryData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}
[Serializable]
public class QuickSkillSlotsData : InventoryData
{
    public int inventoryCount = 6;
    public QuickSkillSlotsData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}
public class QuickPortionSlotsData : InventoryData
{
    public int inventoryCount = 4;
    public QuickPortionSlotsData()
    {
        if (slotDatas == null || slotDatas.Count == 0)
        {
            InitializeSlots(inventoryCount);
        }
        else
        {
            Debug.Log(slotDatas.Count);
        }
    }
}