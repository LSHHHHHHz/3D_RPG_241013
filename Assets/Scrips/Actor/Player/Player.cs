using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private Weapon equippedWeapon;

    public void EquipWeapon(Weapon newWeapon)
    {
        equippedWeapon = newWeapon;
    }
    public void Attack()
    {
        equippedWeapon?.Attack();
    }
    public void SwapWeapon(Weapon newWeapon)
    {
        EquipWeapon(newWeapon);
    }
}
