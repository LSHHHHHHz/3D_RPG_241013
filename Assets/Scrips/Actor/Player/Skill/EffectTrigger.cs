using UnityEngine;

public class EffectTrigger : MonoBehaviour
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
            SendDamageEvent damage = new SendDamageEvent(this.damage);
            enemy.ReceiveEvent(damage);
        }
    }
}
