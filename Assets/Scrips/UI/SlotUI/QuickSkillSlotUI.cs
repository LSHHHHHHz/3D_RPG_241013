using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickSkillSlotUI : BaseSlotUI, IDropHandler
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
    private void OnEnable()
    {
        onSetData += GameManager.instance.skillManager.SetSkill;
    }
    private void OnDisable()
    {
        onSetData -= GameManager.instance.skillManager.SetSkill;
    }
    void ClickButton(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            var skill = GameManager.instance.skillManager.GetSkill(id);
            if (skill != null)
            {
                skill.ExcuteSkill();
            }
            else
            {
                Debug.Log("스킬 없음");
            }
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
        for(int i =0; i< GameData.instance.quickSkillSlotsData.slotDatas.Count; i++)
        {
            if(db.dataID == GameData.instance.quickSkillSlotsData.slotDatas[i].dataID)
            {
                return;
            }
        }
        currentSlotData.SetData(db.dataID, 0);
    }
}