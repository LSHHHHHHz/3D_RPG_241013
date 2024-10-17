using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] GameObject shopSlosPrefab;
    public List<string> dataIDList = new List<string>();
    private void Start()
    {
        dataIDList = GameManager.instance.gameDB.GetDataID(npcName);
        for(int i =0; i < dataIDList.Count; i++)
        {
            ShopSlotUI shopSlotUI = Instantiate(shopSlosPrefab, transform).GetComponent<ShopSlotUI>();
            shopSlotUI.SetData(dataIDList[i]);
        }
    }
}
