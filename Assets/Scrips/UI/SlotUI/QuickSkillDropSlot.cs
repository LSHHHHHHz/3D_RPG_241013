using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickSkillDropSlot : DropSlot
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
    protected override void ClickButton()
    {
        skill.ExcuteSkill();
    }
}