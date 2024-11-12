public interface ISkillStrategy
{
    void ExcuteSkill(IEventReceiver target, int damage);
}
public class PlayerSkillStrategy : ISkillStrategy
{
    public void ExcuteSkill(IEventReceiver target, int damage)
    {
        if(target is BaseEnemy enemy)
        {
            SendDamageEvent damageEvent = new SendDamageEvent(damage);
            target.ReceiveEvent(damageEvent);
        }
    }
}
public class EnemySkillStrategy : ISkillStrategy
{
    public void ExcuteSkill(IEventReceiver target, int damage)
    {
        if (target is Player player)
        {
            SendDamageEvent damageEvent = new SendDamageEvent(damage);
            target.ReceiveEvent(damageEvent);
        }
    }
}