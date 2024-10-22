using System;
using System.Data;

[Serializable]
public class QuestEntity
{
    public string QuestID;
    public string npcID;
    public string questName;
    public string questDescription; 
    public int questClearCount; 
    public int currentQuestClearCount;
    public string questDialog;
    public bool isCompleted;
}
