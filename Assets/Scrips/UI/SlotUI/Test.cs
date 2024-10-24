using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public BaseSlotUI dropSlotUI;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            dropSlotUI.SetData("HPPortion1", 5);
        });
    }
}
