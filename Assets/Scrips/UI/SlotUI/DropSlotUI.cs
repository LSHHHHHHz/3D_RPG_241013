using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropSlotUI : MonoBehaviour, IDropHandler
{
    public SlotData currentSlotData { get; set; }
    public string currentDataID { get; private set; }
    public int currentDataCount { get; private set; }
    [SerializeField] Image dataImage;
    [SerializeField] Text dataCount;
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
            currentDataID = dataID;
            currentDataCount = 0;
            dataImage.sprite = null;
            dataCount.text = "";
            return;
        }
        currentDataID = dataID;
        currentDataCount = count;
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(dataID);
        dataImage.sprite = Resources.Load<Sprite>(dataID);
        dataCount.text = count.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragSlotUI draggedSlot = eventData.pointerDrag.GetComponent<DragSlotUI>();
        if (draggedSlot != null)
        {
        }
    }
}
