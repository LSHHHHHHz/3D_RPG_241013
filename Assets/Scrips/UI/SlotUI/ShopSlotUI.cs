using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text itemName;
    [SerializeField] Text iemDescription;
    [SerializeField] Text itemPrice;
    [SerializeField] GameObject shopBuyPopupPrefab;
    ShopBuyPopupUI shopBuyPopupUI;
    GameDBEntity db;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            OpenShopBuyPopup();
        });
    }
    public void SetData(string dataID)
    {
        db = GameManager.instance.gameDB.GetProfileDB(dataID);
        itemImage.sprite = Resources.Load<Sprite>(db.iconPath);
        itemName.text = db.name;
        iemDescription.text = db.description;
        itemPrice.text = db.price.ToString();
    }
    void OpenShopBuyPopup()
    {
        if(shopBuyPopupUI == null)
        {
            shopBuyPopupUI = Instantiate(shopBuyPopupPrefab, transform).GetComponent<ShopBuyPopupUI>();
        }
        else
        {
            shopBuyPopupUI.gameObject.SetActive(true);
        }
        shopBuyPopupUI.SetData(db);
    }
}
