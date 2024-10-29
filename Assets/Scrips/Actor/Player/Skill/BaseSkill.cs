using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseSkill : MonoBehaviour
{
    public string skillId { get; private set; }
    public float skillAmount {  get; private set; }
    public void SetSkillData(string id, float amount)
    {
        skillId = id;
        skillAmount = amount;
    }
    public abstract void ExcuteSkill(Actor actor);
}
