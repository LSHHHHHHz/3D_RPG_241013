using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon 
{
    public string name;
    public int damage;

    public abstract void Attack();
}
public class OneHandedWeapon : Weapon
{
    public override void Attack()
    {
        Debug.Log("�Ѽչ���");
    }
}

public class TwoHandedWeapon : Weapon
{
    public override void Attack()
    {
        Debug.Log("��չ���");
    }
}
