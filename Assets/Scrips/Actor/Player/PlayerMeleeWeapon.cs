using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMeleeWeapon : ActorMeleeWeapon<BaseEnemy>
{
    private void OnEnable()
    {
        EventManager.instance.onEndPlayerAttackAnim += ResetTarget;
        EventManager.instance.onStartPlayerAttackAnim += StartAttackAction;
    }
    private void OnDisable()
    {
        EventManager.instance.onEndPlayerAttackAnim -= ResetTarget;
        EventManager.instance.onStartPlayerAttackAnim -= StartAttackAction;
    }
    public override void Update()
    {
        base.Update();
        targets = ActorManager<BaseEnemy>.instnace.GetActors();
    }
}
