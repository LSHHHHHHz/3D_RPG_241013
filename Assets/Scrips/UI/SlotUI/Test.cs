using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public DropSlot dropSlotUI;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            dropSlotUI.SetData("Skill1", 5);
        });
    }
}
