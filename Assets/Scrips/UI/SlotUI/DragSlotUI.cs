using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DropSlotUI dropSlotUI { get; private set; }
    public string dragDataId { get; private set; }
    public int dragDataCount {  get; private set; }
    private Transform canvas;
    private Transform previousParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    [SerializeField] Image dataImage;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>().transform;
        dropSlotUI = GetComponentInParent<DropSlotUI>();
    }
    private void OnEnable()
    {
        dropSlotUI.onSetData += SetData;
    }
    private void OnDisable()
    {
        dropSlotUI.onSetData -= SetData;
    }
    public void SetData(string dataID)
    {
        if(string.IsNullOrEmpty(dataID))
        {
            dataImage.sprite = null;
            return;
        }
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(dataID);
        dataImage.sprite = Resources.Load<Sprite>(db.iconPath);
    }
    void SetDragData(string id, int count)
    {
        dragDataId = id;
        dragDataCount = count;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;// 드래그 중일 때 Raycast 비활성화
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
        SetDragData(dropSlotUI.currentSlotData.dataID, dropSlotUI.currentSlotData.count);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rectTransform.position = previousParent.GetComponent<RectTransform>().position;
            //if (!RectTransformUtility.RectangleContainsScreenPoint(slotsRectTransform, eventData.position, null))
            //{

            //}
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
