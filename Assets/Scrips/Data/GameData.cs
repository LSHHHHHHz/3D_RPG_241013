using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    private static GameData _instance;
    public static GameData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public ItemInventoryData itemInventoryData;
    public ItemEquipInventoryData firstItemEquipInventoryData;
    public ItemEquipInventoryData secondItemEquipInventoryData;
    public QuickPortionSlotsData quickPortionSlotsData;
    public QuickSkillSlotsData quickSkillSlotsData;
    public ActiveSkillInventoryData activeSkillInventoryData;
    public PassiveSkillInventoryData passiveSkillInventoryData;
    public GameData()
    {
        itemInventoryData = new ItemInventoryData();
        firstItemEquipInventoryData = new ItemEquipInventoryData();
        secondItemEquipInventoryData = new ItemEquipInventoryData();
        quickPortionSlotsData = new QuickPortionSlotsData();
        quickSkillSlotsData = new QuickSkillSlotsData();
        activeSkillInventoryData = new ActiveSkillInventoryData();
        passiveSkillInventoryData = new PassiveSkillInventoryData();
    }
    [ContextMenu("Save To Json Data")]
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.dataPath, "UserData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Load From Json Data")]
    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "UserData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, instance);
        }
    }
}