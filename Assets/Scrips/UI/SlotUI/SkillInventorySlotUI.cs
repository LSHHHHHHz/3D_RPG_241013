using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillInventorySlotUI : BaseSlotUI
{
    public event Action<string> onClickButton;
    [SerializeField] Button button;
    private void Awake()
    {
        button.onClick.AddListener(()=>ClickButton(currentSlotData.dataID));
    }
    void ClickButton(string id)
    {
        onClickButton?.Invoke(id);
    }
}