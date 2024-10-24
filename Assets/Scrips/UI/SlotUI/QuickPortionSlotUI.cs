using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickPortionSlotUI : BaseSlotUI, IDropHandler
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(currentSlotData.dataID))
            {
                ClickButton(currentSlotData.dataID);
            }
        });
    }
    void ClickButton(string dataID)
    {
        currentSlotData.UsePortion();
        if(!string.IsNullOrEmpty(currentSlotData.dataID))
        {
            EventManager.instance.RecoverHP(GameManager.instance.gameDB.GetProfileDB(currentSlotData.dataID).amount);
        }
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
        if (currentSlotData.dataID == draggedSlot.dragDataId)
        {
            currentSlotData.MergeData(currentSlotData, draggedSlot.dropSlotUI.currentSlotData);
        }
        else
        {
            currentSlotData.SwapData(currentSlotData, draggedSlot.dropSlotUI.currentSlotData);
        }
    }
}