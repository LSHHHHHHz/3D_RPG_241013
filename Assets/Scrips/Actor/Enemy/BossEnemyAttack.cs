using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossEnemyAttack : BaseEnemyAttack
{
    private int actionType = -1;
    [SerializeField] float excuteSpecialAttack = 0.5f;
    [SerializeField] GameObject projectilePrefab;
    BaseSkill baseSkill;
    public override void AttackAction()
    {
        actionType = Random.Range(0, 3);

        switch (actionType)
        {
            case 0:
                Heal();
                break;
            case 1:
                BasicAttack();
                break;
            case 2:
                SpecialAttack();
                break;
        }
        actionType = -1;
    }
    private void Heal()
    {
        enemy.fsmController.ChangeState(new EnemyHealState());
    }
    private void BasicAttack()
    {
        enemy.fsmController.ChangeState(new EnemyAttackState());
    }
    private void SpecialAttack()
    {
        enemy.fsmController.ChangeState(new EnemySpecialAttackState());
    }
    public void ExcuteProjectileAttack(AnimatorStateInfo info)
    {
        if (info.IsName("Enemy_Skill") && info.normalizedTime >= excuteSpecialAttack)
        {
            if(baseSkill == null)
            {
                baseSkill = Instantiate(projectilePrefab).GetComponent<BaseSkill>();
                baseSkill.ExcuteSkill(GetComponent<BaseEnemy>());
            }
            else
            {
                baseSkill.ExcuteSkill(GetComponent<BaseEnemy>());
            }
        }
    }
}
