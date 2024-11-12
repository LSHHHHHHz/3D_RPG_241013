using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyPopupUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text itemName;
    [SerializeField] Text totalPrice;
    [SerializeField] Text count;
    [SerializeField] Button increaseCountButton;
    [SerializeField] Button decreaseCountButton;
    [SerializeField] Button buyItemButton;
    [SerializeField] Button closePopupButton;
    Player player;
    private int currentCount = 1;
    private int itemPrice;
    private string dataID;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        increaseCountButton.onClick.AddListener(() =>
        {
            IncreaseCount();
        });
        decreaseCountButton.onClick.AddListener(() =>
        {
            DecreaseCount();
        });
        buyItemButton.onClick.AddListener(() =>
        {
            BuyItem(dataID, currentCount);
        });
        closePopupButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void SetData(GameDBEntity db)
    {
        dataID = db.dataID;
        itemImage.sprite = Resources.Load<Sprite>(db.iconPath);
        itemName.text = db.name;
        itemPrice = db.price;
        currentCount = 1;
        UpdateUI();
    }
    private void IncreaseCount()
    {
        currentCount++;
        UpdateUI();
    }
    private void DecreaseCount()
    {
        if (currentCount > 1)
        {
            currentCount--;
            UpdateUI();
        }
    }

    private void BuyItem(string id, int count)
    {
        int totalCost = currentCount * itemPrice;

        if (player.currency.coin >= totalCost)
        {
            gameObject.SetActive(false);
            player.currency.SpendCoin(totalCost);
            for (int i = 0; i < GameData.instance.itemInventoryData.slotDatas.Count; i++)
            {
                if (string.IsNullOrEmpty(GameData.instance.itemInventoryData.slotDatas[i].dataID))
                {
                    GameData.instance.itemInventoryData.slotDatas[i].SetData(id, count);
                    break;
                }
            }
        }
    }
    private void UpdateUI()
    {
        count.text = currentCount.ToString();
        totalPrice.text = (currentCount * itemPrice).ToString();
    }
}
