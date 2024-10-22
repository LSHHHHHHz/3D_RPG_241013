using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<QuestEntity> mainQuests = new List<QuestEntity>();
    public List<QuestEntity> subQuests = new List<QuestEntity>();

    public int mainQuestIndex = 0;
    public int subQuestIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ClearMainQuest()
    {
        if (mainQuestIndex < mainQuests.Count)
        {
            mainQuests[mainQuestIndex].isCompleted = true;
            mainQuestIndex++;
        }
    }
    public void ClearSubQuest()
    {
        if (subQuestIndex < subQuests.Count)
        {
            subQuests[subQuestIndex].isCompleted = true;
            subQuestIndex++;
        }
    }
    public QuestEntity GetCurrentMainQuest()
    {
        if (mainQuestIndex < mainQuests.Count)
        {
            return mainQuests[mainQuestIndex];
        }
        return null;
    }
    public QuestEntity GetCurrentSubQuest()
    {
        if (subQuestIndex < subQuests.Count)
        {
            return subQuests[subQuestIndex];
        }
        return null;
    }
}
