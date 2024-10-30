using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSkill2Projectile : MonoBehaviour
{
    private int damage;

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }
    private void OnTriggerEnter(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            SendDamageEvent damage = new SendDamageEvent(20);
            enemy.ReceiveEvent(damage);
        }
    }
}
