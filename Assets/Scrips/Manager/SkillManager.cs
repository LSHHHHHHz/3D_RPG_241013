using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillManager : MonoBehaviour
{
    List<GameObject> skillPrefabs = new List<GameObject>();
    List<BaseSkill> skills = new List<BaseSkill>();
    public void SetSkill(string id)
    {
        GameObject existingPrefab = null;
        foreach (var prefab in skillPrefabs)
        {
            BaseSkill skill = prefab.GetComponent<BaseSkill>();
            if (skill != null && skill.skillId == id)
            {
                existingPrefab = prefab;
                break;
            }
        }
        if (existingPrefab != null)
        {
            existingPrefab.SetActive(true);
        }
        else
        {
            GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(id);
            if (db.dataType != "active" && db.dataType != "passive")
            {
                return;
            }

            GameObject newSkillPrefab = Instantiate(Resources.Load<GameObject>(db.prefabPath));
            skillPrefabs.Add(newSkillPrefab);

            BaseSkill skill = newSkillPrefab.GetComponent<BaseSkill>();
            if (skill != null)
            {
                skills.Add(skill);
            }
        }
    }
    public BaseSkill GetSkill(string id)
    {
        foreach (var skill in skills)
        {
            if (id == skill.skillId)
            {
                return skill;
            }
        }
        return null;
    }
}