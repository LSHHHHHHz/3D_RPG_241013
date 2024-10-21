using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] GameObject shopSlotPrefab;
    private List<GameObject> createdSlots = new List<GameObject>();
    public List<string> originDataIDList = new List<string>();
    public List<string> dataIDList = new List<string>();
    public void OpenShopPopup(string name)
    {
        npcName = name;
        dataIDList = GameManager.instance.gameDB.GetDataID(npcName);
        if (originDataIDList.Count != dataIDList.Count)
        {
            for (int i = 0; i < createdSlots.Count; i++)
            {
                Destroy(createdSlots[i]);
            }
            createdSlots.Clear();
        }
        for (int i = 0; i < dataIDList.Count; i++)
        {
            GameObject newSlot = Instantiate(shopSlotPrefab, transform);
            createdSlots.Add(newSlot);
            ShopSlotUI shopSlotUI = newSlot.GetComponent<ShopSlotUI>();
            shopSlotUI.SetData(dataIDList[i]);
        }
    }
}
