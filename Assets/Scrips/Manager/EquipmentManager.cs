using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Transform equipShieldPoint;
    public Transform stowingShieldPoint;
    public Transform equipSwordPoint;
    public Transform stowingWeaponPoint;

    private GameObject currentWeapon; 
    private GameObject currentShield;

    public void EquipItem(string itemId)
    {
        GameDBEntity itemData = GameManager.instance.gameDB.GetProfileDB(itemId);

        if (itemData.dataType == "Weapon")
        {
            EquipWeapon(itemData);
        }
        else if (itemData.dataType == "Shield")
        {
            EquipShield(itemData);
        }
    }
    private void EquipWeapon(GameDBEntity weaponData)
    {
        if (currentWeapon != null)
        {
            StowItem(currentWeapon, stowingWeaponPoint);
        }
        currentWeapon = Instantiate(Resources.Load<GameObject>(weaponData.prefabPath), equipSwordPoint);
    }
    private void EquipShield(GameDBEntity shieldData)
    {
        if (currentShield != null)
        {
            StowItem(currentShield, stowingShieldPoint);
        }
        currentShield = Instantiate(Resources.Load<GameObject>(shieldData.prefabPath), equipShieldPoint);
    }
    private void StowItem(GameObject item, Transform stowingPoint)
    {
        item.transform.SetParent(stowingPoint);
    }
}
