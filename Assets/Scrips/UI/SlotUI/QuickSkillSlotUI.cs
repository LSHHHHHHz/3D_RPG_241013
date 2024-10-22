using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickSkillSlotUI : BaseSlotUI
{
    GameObject skillPrefab;
    BaseSkill skill;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(currentSlotData.dataID))
            {
                ClickButton();
            }
        });
    }
    private void OnEnable()
    {
        onSetData += SetQuickSkillSlot;
    }
    private void OnDisable()
    {
        onSetData -= SetQuickSkillSlot;
    }
    void SetQuickSkillSlot(string id)
    {
        if(skillPrefab != null)
        {
            skillPrefab.SetActive(false);
        }
        if (!string.IsNullOrEmpty(id))
        {
            GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(id);
            if(db.dataType != "Skill")
            {
                return;
            }
            if (skillPrefab == null)
            {
                skillPrefab = Instantiate(Resources.Load<GameObject>(db.prefabPath));
            }
            else
            {
                skillPrefab.SetActive(true);
            }
            skill = skillPrefab.GetComponent<BaseSkill>();
        }
    }
    void ClickButton()
    {
        skill.ExcuteSkill();
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