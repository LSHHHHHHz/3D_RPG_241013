using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[ExcelAsset]
public class GameDB : ScriptableObject
{
	public List<DialogDBEntity> DialogEntites;
	public List<GameDBEntity> GameDataEntites;
    public List<ShopDB> shopDB;
    public List<QuestEntity> QuestEntities;
    public List<MonsterEntity> MonsterEntites;
    public GameDBEntity GetProfileDB(string id)
    {
        foreach (GameDBEntity profile in GameDataEntites)
        {
            if (id == profile.dataID)
            {
                return profile;
            }
        }
        Debug.LogError("맞는 아이디 없음");
        return null;
    }
    public List<string> GetDataID(string id)
    {
        List<string> list = new List<string>();
        foreach (ShopDB db in shopDB)
        {
            if (id == db.npcID)
            {
                list.Add(db.dataID);
            }
        }
        return list;
    }
    public MonsterEntity GetEnemyProfileDB(string id)
    {
        foreach (MonsterEntity profile in MonsterEntites)
        {
            if (id == profile.monsterID)
            {
                return profile;
            }
        }
        Debug.LogError("맞는 아이디 없음");
        return null;
    }
}
