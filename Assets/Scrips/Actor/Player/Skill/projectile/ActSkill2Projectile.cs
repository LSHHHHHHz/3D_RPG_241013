using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSkill2Projectile : MonoBehaviour
{
    private int damage;
    private ISkillStrategy skillStrategy;

    public void SetSkill(ISkillStrategy skillStrategy, int dmg)
    {
        this.skillStrategy = skillStrategy;
        this.damage = dmg;
    }
    private void OnTriggerEnter(Collider other)
    {
        IEventReceiver target = other.GetComponent<IEventReceiver>();
        if (target != null && skillStrategy != null)
        {
            skillStrategy.ExcuteSkill(target, damage);
        }
    }
}
