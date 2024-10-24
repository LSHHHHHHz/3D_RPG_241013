using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EquipmentSlotUI : BaseSlotUI, IDropHandler
{
    public event Action<string> onEquipItem;
    private void OnEnable()
    {
        onEquipItem += GameManager.instance.equipmentManager.EquipItem;
    }
    private void OnDisable()
    {
        onEquipItem -= GameManager.instance.equipmentManager.EquipItem;
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragSlotUI draggedSlot = eventData.pointerDrag.GetComponent<DragSlotUI>();
        if (string.IsNullOrEmpty(draggedSlot.dragDataId))
        {
            return;
        }
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(draggedSlot.dragDataId);
        if (IsPossibleDrop(db.dataType) == false)
        {
            return;
        }
        currentSlotData.SetData(db.dataID, 0);
        onEquipItem?.Invoke(db.dataID);
    }
}