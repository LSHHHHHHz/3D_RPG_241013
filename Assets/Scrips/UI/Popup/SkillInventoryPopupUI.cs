using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class SkillInventoryPopupUI : MonoBehaviour
{
    [SerializeField] Text skillName;
    [SerializeField] Text skillRequiredLV;
    [SerializeField] Text skillDescription;
    private void OnEnable()
    {
        EventManager.instance.PossibleAttack(false);
        Initialized();
    }
    private void Initialized()
    {
        skillName.text = "";
        skillRequiredLV.text = "";
        skillDescription.text = "";
    }
    public void SetData(string id)
    {
        GameDBEntity skill = GameManager.instance.gameDB.GetProfileDB(id);
        skillName.text = skill.name;
        skillRequiredLV.text = "필요레벨 : " + skill.requiredLV;
        skillDescription.text = skill.description;
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
        EventManager.instance.PossibleAttack(true);
    }
}
