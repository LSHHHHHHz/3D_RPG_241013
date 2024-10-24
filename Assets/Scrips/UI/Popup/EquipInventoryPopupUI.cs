using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipInventoryPopupUI : MonoBehaviour
{
    [SerializeField] EquipmentInventoryUI[] equipmentInventorys;
    [SerializeField] Button[] buttons;
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() =>
            {
                ActivateSlotPanel(index);

            });
        }
    }
    void ActivateSlotPanel(int index)
    {
        for (int i = 0; i < equipmentInventorys.Length; i++)
        {
            equipmentInventorys[i].gameObject.SetActive(i == index);
        }
    }
}
