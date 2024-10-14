using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActorMeleeAttack : MonoBehaviour
{
    public int damage = 20; 
    public float attackRange = 1.5f;
    public float attackWidth = 0.5f;
    IReadOnlyList<BaseEnemy> enemies;
    private void Update()
    {
        enemies = ActorManager<BaseEnemy>.instnace.GetActors();
        SendDamage();
    }

    private void SendDamage()
    {
        foreach (BaseEnemy enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= attackRange)
            {
                Debug.Log(enemy.name);
            }
        }
    }
}
