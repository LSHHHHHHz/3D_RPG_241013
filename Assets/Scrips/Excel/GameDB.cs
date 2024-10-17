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
}
