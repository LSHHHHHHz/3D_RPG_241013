using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    itemInventory,
    EquipImentnventory,
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
    public int slotCount;
    protected List<DropSlot> slots;

    protected virtual void Awake()
    {
        slots = new List<DropSlot>();
    }
    public void AddSlot()
    {
        DropSlot newSlot = Instantiate(slotPrefab, slotTransform).GetComponent<DropSlot>();
        newSlot.parentInventoryType = inventoryType;
        slots.Add(newSlot);
    }
    public void SetSlotCount(int newCount)
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
