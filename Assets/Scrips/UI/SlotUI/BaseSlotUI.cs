using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BaseSlotUI : MonoBehaviour
{
    public SlotData currentSlotData { get; set; }
    public InventoryType parentInventoryType;
    [SerializeField] Image dataImage;
    [SerializeField] Text dataCount;

    public event Action onDropSlot;
    public event Action<string> onSetData;
    public void SetData(string dataID, int count)
    {
        if (string.IsNullOrEmpty(dataID))
        {
            currentSlotData.dataID = dataID;
            currentSlotData.count = 0;
            dataImage.sprite = null;
            if (dataCount != null)
            {
                dataCount.text = "";
            }
            dataImage.color = new Color(1, 1, 1, 0);
            onSetData?.Invoke(currentSlotData.dataID);
            return;
        }
        dataImage.color = new Color(1, 1, 1, 1);
        currentSlotData.dataID = dataID;
        currentSlotData.count = count;
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(dataID);
        dataImage.sprite = Resources.Load<Sprite>(db.iconPath);
        if (dataCount != null)
        {
            dataCount.text = count.ToString();
        }
        onSetData?.Invoke(currentSlotData.dataID);
    }    
    protected bool IsPossibleDrop(string type)
    {        
        switch (parentInventoryType)
        {
            case InventoryType.itemInventory:
                return type != "Skill";
            case InventoryType.QuickPortionSlots:
                return type == "Portion"; 
            case InventoryType.QuickSkillSlots:
                return type == "active" || type == "passive";
            case InventoryType.SwordEquipIment:
                return type == "Sword";
            case InventoryType.ShieldEquipment:
                return type == "Shield";
            default:
                return false;  
        }
    }
}
