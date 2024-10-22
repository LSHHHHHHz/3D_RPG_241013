using System;

[Serializable]
public class QuestEntity
{
    public string QuestID;
    public string npcID;
    public string questName;
    public string questDescription; 
    public int questClearCount; 
    public int currentQuestClearCount;
    public bool isCompleted;
}
