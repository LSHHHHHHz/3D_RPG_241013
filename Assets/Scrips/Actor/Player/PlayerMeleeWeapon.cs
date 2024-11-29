using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public class PlayerMeleeWeapon : ActorMeleeWeapon<BaseEnemy>, IEquipment
{
    PlayerAnimation playerAnim;
    Player player;
    string weaponID;
    bool isEquipped = false;
    private void OnEnable()
    {
        playerAnim = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<Player>();
        playerAnim.onEndPlayerAttackAnim += ResetTarget;
        playerAnim.onStartPlayerAttackAnim += StartAttackAction;
        player.stats.onChangeAttack += SetTotalDamage;
        player.stats.InitializeStats();
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
        isPossibleAttackanim = player.isPossbleAttack;
    }
    void SetTotalDamage(int damage)
    {
        totalDamage = damage;
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
        if (!isEquipped)
        {
            gameObject.SetActive(true);
            int attackBonus = GameManager.instance.gameDB.GetProfileDB(id).amount;
            player.stats.IncreaseAttack(attackBonus);
            isEquipped = true;
        }
    }

    public void UnEquipItem(string id)
    {
        if (isEquipped)
        {
            int attackBonus = GameManager.instance.gameDB.GetProfileDB(id).amount;
            player.stats.DecreaseAttack(attackBonus);
            isEquipped = false;
            gameObject.SetActive(false);
        }
    }
}
