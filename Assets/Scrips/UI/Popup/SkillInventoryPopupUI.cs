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
    private void OnEnable()
    {
        EventManager.instance.PossibleAttack(false);
    }
    public void SetData(string id)
    {
        GameDBEntity skill = GameManager.instance.gameDB.GetProfileDB(id);
        skillName.text = skill.name;
        skillRequiredLV.text = "�ʿ䷹�� : " + skill.requiredLV;
        skillDescription.text = skill.description;
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
        EventManager.instance.PossibleAttack(true);
    }
}
