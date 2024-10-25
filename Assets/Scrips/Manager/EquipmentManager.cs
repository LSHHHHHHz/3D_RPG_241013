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

    private List<IEquipment> playerEquipedSword = new List<IEquipment>();
    private List<IEquipment> playerEquipedShield = new List<IEquipment>();

    private IEquipment currentEquipSword;
    private IEquipment currentEquipShield;

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
        IEquipment currentEquiped = null;

        for (int i = 0; i < playerEquipedSword.Count; i++)
        {
            if (playerEquipedSword[i].GetItemID() == weaponData.dataID)
            {
                currentEquiped = playerEquipedSword[i];
                break;
            }
        }

        if (currentEquiped != null)
        {
            foreach (IEquipment sword in playerEquipedSword)
            {
                sword.UnEquipItem(sword.GetItemID());
            }
            currentEquipSword = currentEquiped;
            currentEquipSword.EquipItem(currentEquipSword.GetItemID());
        }
        else
        {
            foreach (IEquipment sword in playerEquipedSword)
            {
                sword.UnEquipItem(sword.GetItemID());
            }
            GameObject prefab = Resources.Load<GameObject>(weaponData.prefabPath);
            IEquipment newSword = Instantiate(prefab, equipSwordPoint).GetComponent<IEquipment>();
            newSword.SetItemID(weaponData.dataID);
            playerEquipedSword.Add(newSword);

            currentEquipSword = newSword;
            currentEquipSword.EquipItem(currentEquipSword.GetItemID());
        }
    }
    private void EquipShield(GameDBEntity shieldData)
    {
        IEquipment currentEquiped = null;

        for (int i = 0; i < playerEquipedShield.Count; i++)
        {
            if (playerEquipedShield[i].GetItemID() == shieldData.dataID)
            {
                currentEquiped = playerEquipedShield[i];
                break;
            }
        }
        if (currentEquiped != null)
        {
            foreach (IEquipment shield in playerEquipedShield)
            {
                shield.UnEquipItem(shield.GetItemID());
            }
            currentEquipShield = currentEquiped;
            currentEquipShield.EquipItem(currentEquipShield.GetItemID());
        }
        else
        {
            foreach (IEquipment shield in playerEquipedShield)
            {
                shield.UnEquipItem(shield.GetItemID());
            }

            GameObject prefab = Resources.Load<GameObject>(shieldData.prefabPath);
            IEquipment newShield = Instantiate(prefab, equipShieldPoint).GetComponent<IEquipment>();
            newShield.SetItemID(shieldData.dataID);
            playerEquipedShield.Add(newShield);

            currentEquipShield = newShield;
            currentEquipShield.EquipItem(currentEquipShield.GetItemID());
        }
    }
}
