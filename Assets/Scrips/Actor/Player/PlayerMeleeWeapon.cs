using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMeleeWeapon : ActorMeleeWeapon<BaseEnemy>
{
    PlayerAnimation playerAnim;

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
}
