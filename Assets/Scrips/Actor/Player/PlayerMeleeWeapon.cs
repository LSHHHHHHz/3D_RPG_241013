using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMeleeWeapon : ActorMeleeWeapon<BaseEnemy>, IEquipment
{
    PlayerAnimation playerAnim;
    string weaponID;
    private void OnEnable()
    {
        playerAnim = GetComponentInParent<PlayerAnimation>();
        playerAnim.onEndPlayerAttackAnim += ResetTarget;
        playerAnim.onStartPlayerAttackAnim += StartAttackAction;
    }
    private void OnDisable()
    {
        playerAnim.onEndPlayerAttackAnim -= ResetTarget;
        playerAnim.onStartPlayerAttackAnim -= StartAttackAction;
    }
    public override void Update()
    {
        base.Update();
        targets = ActorManager<BaseEnemy>.instnace.GetActors();
    }

    public void SetItemID(string id)
    {
        weaponID = id;
    }

    public string GetItemID()
    {
        return weaponID;
    }

    public void EquipItem(string id)
    {
        Debug.Log("아이템 장착");
    }

    public void UnEquipItem(string id)
    {
        Debug.Log("아이템 해제");
    }
}
