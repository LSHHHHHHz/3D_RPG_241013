using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemInventoryPopupUI : BaseInventory
{
    private ItemInventoryData itemInventoryData;
    [SerializeField] Transform equipPopup;
    [SerializeField] Transform openEquipPopup;
    [SerializeField] Transform closeEquipPopup;
    private void OnEnable()
    {
        EventManager.instance.PossibleAttack(false);
    }
    protected override void Awake()
    {
        base.Awake();
        itemInventoryData = GameData.instance.itemInventoryData;
        SetSlotCount(itemInventoryData.inventoryCount);
        SetSlotData();
    }
    void SetSlotData()
    {
        for (int i = 0; i < itemInventoryData.inventoryCount; i++)
        {
            slots[i].currentSlotData = itemInventoryData.slotDatas[i];
            itemInventoryData.slotDatas[i].onDataChanged += slots[i].SetData;
            itemInventoryData.slotDatas[i].SetData(itemInventoryData.slotDatas[i].dataID, itemInventoryData.slotDatas[i].count);
        }
    }
    public void OpenEquipPopup()
    {
        openEquipPopup.gameObject.SetActive(false);
        closeEquipPopup.gameObject.SetActive(true);
        equipPopup.gameObject.SetActive(true);
    }
    public void CloseEquipPopup()
    {
        openEquipPopup.gameObject.SetActive(true);
        closeEquipPopup.gameObject.SetActive(false);
        equipPopup.gameObject.SetActive(false);
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
        EventManager.instance.PossibleAttack(true);
    }
}
