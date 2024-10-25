using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    void SetItemID(string id);
    string GetItemID();
    void EquipItem(string id);
    void UnEquipItem(string id);
}

public class EquipmentManager : MonoBehaviour
{
    public Transform equipSwordPoint;
    public Transform equipShieldPoint;

    private List<GameObject> playerEquipedSword = new List<GameObject>();
    private List<GameObject> playerEquipedShield = new List<GameObject>();

    private GameObject currentEquipSword;
    private GameObject currentEquipShield;

    public void EquipItem(string itemId)
    {
        GameDBEntity itemData = GameManager.instance.gameDB.GetProfileDB(itemId);

        if (itemData.dataType == "sword")
        {
            EquipSword(itemData);
        }
        else if (itemData.dataType == "shield")
        {
            EquipShield(itemData);
        }
    }
    private void EquipSword(GameDBEntity weaponData)
    {
        GameObject existingSword = null;
        for (int i = 0; i < playerEquipedSword.Count; i++)
        {
            if (playerEquipedSword[i].GetComponent<IEquipment>().GetItemID() == weaponData.dataID)
            {
                existingSword = playerEquipedSword[i];
                break;
            }
        }
        if (existingSword != null)
        {
            foreach (GameObject sword in playerEquipedSword)
            {
                sword.SetActive(false);
            }
            currentEquipSword = existingSword;
            currentEquipSword.SetActive(true);
        }
        else
        {
            foreach (GameObject sword in playerEquipedSword)
            {
                sword.SetActive(false);
            }
            GameObject prefab = Resources.Load<GameObject>(weaponData.prefabPath);
            GameObject newSword = Instantiate(prefab, equipSwordPoint);
            IEquipment equip = newSword.GetComponent<IEquipment>();
            equip.SetItemID(weaponData.dataID);
            playerEquipedSword.Add(newSword);

            currentEquipSword = newSword;
            //currentEquipSword.SetActive(true);
        }
    }
    private void EquipShield(GameDBEntity shieldData)
    {
        GameObject existingShield = null;
        for (int i = 0; i < playerEquipedShield.Count; i++)
        {
            if (playerEquipedShield[i].GetComponent<IEquipment>().GetItemID() == shieldData.dataID)
            {
                existingShield = playerEquipedShield[i];
                break;
            }
        }
        if (existingShield != null)
        {
            foreach (GameObject shield in playerEquipedShield)
            {
                shield.SetActive(false);
            }
            currentEquipShield = existingShield;
            currentEquipShield.SetActive(true);
        }
        else
        {
            foreach (GameObject shield in playerEquipedShield)
            {
                shield.SetActive(false);
            }

            GameObject prefab = Resources.Load<GameObject>(shieldData.prefabPath);
            GameObject newShield = Instantiate(prefab, equipShieldPoint);
            IEquipment equip = newShield.GetComponent<IEquipment>();
            equip.SetItemID(shieldData.dataID);
            playerEquipedShield.Add(newShield);

            currentEquipShield = newShield;
           //currentEquipShield.SetActive(true);
        }
    }
    public void ActiveEquipPrefab()
    {
        currentEquipSword.SetActive(true);
        currentEquipShield.SetActive(true);
    }
    private void SwapItem()
    {
    }
}
