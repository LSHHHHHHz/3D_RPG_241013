using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuickPortionDropSlot : DropSlot
{
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
    protected override void ClickButton()
    {

    }
}