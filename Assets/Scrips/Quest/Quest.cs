using UnityEngine;

public enum QuestType { Main, Sub }
public enum QuestStatus { Ready, Progress, Completed }
public enum GoalType { TalkNPC, KillMonsters, BuyItem }

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;
    public string[] targetId;
    public string[] goalName;

    public bool IsCompleted()
    {
        return currentAmount >= requiredAmount;
    }

    public void IncreaseProgress()
    {
        currentAmount++;
    }
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questId;
    public string questTitle;
    public QuestType questType;
    public QuestStatus questStatus = QuestStatus.Ready;
    public QuestGoal goal;
    public Quest nextQuest;

    public void StartQuest()
    {
        if (questStatus == QuestStatus.Ready)
        {
            questStatus = QuestStatus.Progress;
            Debug.Log(questTitle + " : 시작");
        }
    }
    public void ProgressQuest()
    {
        if (questStatus == QuestStatus.Progress)
        {
            Debug.Log(questTitle + " : 진행");
            goal.IncreaseProgress();
            if (goal.IsCompleted())
            {
                CompleteQuest();
            }
        }
    }
    public void CompleteQuest()
    {
        if (questStatus == QuestStatus.Progress)
        {
            questStatus = QuestStatus.Completed;
            Debug.Log(questTitle + " : 완료");
            if (nextQuest != null)
            {
                nextQuest.StartQuest();
            }
        }
    }
}