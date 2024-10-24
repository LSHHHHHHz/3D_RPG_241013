using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class SkillInventoryPopupUI : MonoBehaviour
{
    [SerializeField] Text skillName;
    [SerializeField] Text skillRequiredLV;
    [SerializeField] Text skillDescription;
    public void SetData(string id)
    {
        GameDBEntity skill = GameManager.instance.gameDB.GetProfileDB(id);
        skillName.text = skill.name;
        skillRequiredLV.text = "필요레벨 : " + skill.requiredLV;
        skillDescription.text = skill.description;
    }
}
