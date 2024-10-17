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
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("ÆË¾÷ ¶ç¿ì±â");
        });
    }
    public void SetData(string dataID)
    {
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(dataID);
        itemImage.sprite = Resources.Load<Sprite>(dataID);
        itemName.text = db.name;
        iemDescription.text = db.description;
        itemPrice.text = db.price.ToString();
    }
}
