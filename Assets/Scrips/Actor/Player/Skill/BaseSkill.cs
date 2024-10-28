using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseSkill : MonoBehaviour
{
    public string skillId;
    [SerializeField] float skillAmount;
    public abstract void ExcuteSkill();
}
