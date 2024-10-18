using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : Actor
{
    public PlayerStatus status { get; private set; }
    private void Awake()
    {
        status = new PlayerStatus(100, 50);
        Debug.Log("Player가 먼저되나");
    }
    private void OnEnable()
    {
        ActorManager<Player>.instnace.RegisterActor(this);
    }
    private void OnDisable()
    {
        ActorManager<Player>.instnace.UnregisterActor(this);
    }
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
