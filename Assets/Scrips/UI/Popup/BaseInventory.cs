using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    Inventory,
    EquipInventory
}
public class BaseInventory : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform slotTransform;
    public int slotCount;
    protected List<DropSlotUI> slots;

    protected virtual void Awake()
    {
        slots = new List<DropSlotUI>();
    }
    public void AddSlot()
    {
        DropSlotUI newSlot = Instantiate(slotPrefab, slotTransform).GetComponent<DropSlotUI>();
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
