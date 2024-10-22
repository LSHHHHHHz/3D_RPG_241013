using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDButtonManager : MonoBehaviour
{
    [SerializeField] RectTransform popupTransform;
    [SerializeField] GameObject itemInventoryUIPrefab;
    ItemInventoryPopupUI itemInventoryPopupUI;
    [SerializeField] Button iteminventoryPopupButton;

    private void Awake()
    {
        iteminventoryPopupButton.onClick.AddListener(() =>
        {
            if(itemInventoryPopupUI == null)
            {
                itemInventoryPopupUI = Instantiate(itemInventoryUIPrefab, popupTransform).GetComponent<ItemInventoryPopupUI>();
            }
            if (!itemInventoryPopupUI.gameObject.activeSelf)
            {
                itemInventoryPopupUI.gameObject.SetActive(true);
            }
        });
    }
}
