using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] GameObject shopSlotPrefab;
    [SerializeField] Transform prefabTransform;
    private List<GameObject> createdSlots = new List<GameObject>();
    public List<string> originDataIDList = new List<string>();
    public List<string> dataIDList = new List<string>();
    private void OnEnable()
    {
        EventManager.instance.PossibleAttack(false);
    }
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
            GameObject newSlot = Instantiate(shopSlotPrefab, prefabTransform);
            createdSlots.Add(newSlot);
            ShopSlotUI shopSlotUI = newSlot.GetComponent<ShopSlotUI>();
            shopSlotUI.SetData(dataIDList[i]);
        }
    }
    public void ExitPopup()
    {
        gameObject.SetActive(false);
        EventManager.instance.PossibleAttack(true);
    }
}
