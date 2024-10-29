using System;

[Serializable]
public class GameDBEntity
{
    public string dataID;
    public string name;
    public string iconPath; 
    public string prefabPath;
    public string description;
    public int price;
    public string dataType;
    public float coolDown;
    public int requiredLV;
    public int amount;
}
