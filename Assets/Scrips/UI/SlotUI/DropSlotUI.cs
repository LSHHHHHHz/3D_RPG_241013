using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropSlotUI : MonoBehaviour, IDropHandler
{
    public SlotData currentSlotData { get; set; }
    [SerializeField] Image dataImage;
    [SerializeField] Text dataCount;

    public event Action onDropSlot;
    public event Action<string> onSetData;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("ÆË¾÷ ¶ç¿ì±â");
        });
    }
    public void SetData(string dataID, int count)
    {
        if (string.IsNullOrEmpty(dataID))
        {
            currentSlotData.dataID = dataID;
            currentSlotData.count = 0;
            dataImage.sprite = null;
            dataCount.text = "";
            onSetData?.Invoke(currentSlotData.dataID);
            return;
        }
        currentSlotData.dataID = dataID;
        currentSlotData.count = count;
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(dataID);
        dataImage.sprite = Resources.Load<Sprite>(db.iconPath);
        dataCount.text = count.ToString();
        onSetData?.Invoke(currentSlotData.dataID);
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragSlotUI draggedSlot = eventData.pointerDrag.GetComponent<DragSlotUI>();

        if(currentSlotData.dataID == draggedSlot.dragDataId)
        {
            currentSlotData.MergeData(currentSlotData,draggedSlot.dropSlotUI.currentSlotData);
        }
        else
        {
            currentSlotData.SwapData(currentSlotData, draggedSlot.dropSlotUI.currentSlotData);
        }
    }
}
