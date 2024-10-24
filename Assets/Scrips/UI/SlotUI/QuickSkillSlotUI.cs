using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickSkillSlotUI : BaseSlotUI, IDropHandler
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
            if (db.dataType != "active" && db.dataType != "passive")
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
        if (skill != null)
        {
            skill.ExcuteSkill();
        }
        else
        {
            Debug.Log("스킬 없음");
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