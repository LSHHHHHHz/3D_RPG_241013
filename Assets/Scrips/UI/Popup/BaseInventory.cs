using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    itemInventory,
    SwordEquipIment,
    ShieldEquipment,
    SkillInventory,
    QuickSkillSlots,
    QuickPortionSlots,
    none
}
public class BaseInventory : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform slotTransform;
    public InventoryType inventoryType;
    protected List<BaseSlotUI> slots;

    protected virtual void Awake()
    {
        slots = new List<BaseSlotUI>();
    }
    public void AddSlot()
    {
        BaseSlotUI newSlot = Instantiate(slotPrefab, slotTransform).GetComponent<BaseSlotUI>();
        newSlot.parentInventoryType = inventoryType;
        slots.Add(newSlot);
    }
    protected void SetSlotCount(int newCount)
    {
        int currentCount = slots.Count;

        if (newCount > currentCount)
        {
            for (int i = currentCount; i < newCount; i++)
            {
                AddSlot();
            }
        }
        else if (newCount < currentCount)
        {
            for (int i = currentCount - 1; i >= newCount; i--)
            {
                Destroy(slots[i]);
                slots.RemoveAt(i);
            }
        }
    }
}
