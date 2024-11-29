using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Quest currentQuest { get; private set; }
    [SerializeField] List<Quest> questList = new List<Quest>();
    private int currentQuestIndex = 0;
    public event Action<Quest> onStartQuest;
    public event Action<Quest> onFinishQuest;
    public event Action<string> onClearGoalQeust;
    public event Action onClearQuest;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentQuest = questList[currentQuestIndex];
    }
    private void Start()
    {
        StartNextQuest();
    }
    public void StartNextQuest()
    {
        if (currentQuestIndex < questList.Count)
        {
            currentQuest = questList[currentQuestIndex];
            RegisterQuest(currentQuest);
        }
    }
    public void RegisterQuest(Quest quest)
    {
        StartCoroutine(RegisterQuestWithDelay(quest));
    }
    private IEnumerator RegisterQuestWithDelay(Quest quest)
    {
        yield return new WaitForSeconds(4f);

        if (quest.questStatus == QuestStatus.Ready || quest.goal.currentAmount == 0)
        {
            quest.StartQuest();
            onStartQuest?.Invoke(quest);
        }
    }
    public void ProgressQuest(string targetId)
    {
        Quest quest = null;
        bool isQuestValid = false;

        for (int i = 0; i < questList.Count; i++)
        {
            Quest q = questList[i];
            if (q.questStatus == QuestStatus.Progress)
            {
                if (q.goal.targetId.Length > 0)
                {
                    for (int j = 0; j < q.goal.targetId.Length; j++)
                    {
                        if (q.goal.targetId[j] == targetId && j == q.goal.currentAmount)
                        {
                            onClearGoalQeust?.Invoke(q.goal.goalName[j]);
                            isQuestValid = true;
                            quest = q;
                            //마지막 이라면
                            //if (j == q.goal.targetId.Length - 1)
                            //{
                            //    onFinishQuest?.Invoke(currentQuest);
                            //}
                            break;
                        }
                    }
                }
                else
                {
                    quest = q;
                    break;
                }
                if (quest != null)
                {
                    break;
                }
            }
        }
        if (quest != null)
        {
            if (isQuestValid)
            {
                quest.ProgressQuest();
            }
            if (quest.questStatus == QuestStatus.Completed)
            {
                currentQuestIndex++;
                StartNextQuest();
            }
        }
        if(quest != null && quest.goal.currentAmount == quest.goal.requiredAmount -1)
        {
            onClearQuest?.Invoke();
            onFinishQuest?.Invoke(currentQuest);
        }
    }
    public List<Quest> GetQuests()
    {
        List<Quest> activeQuests = new List<Quest>();
        foreach (Quest quest in questList)
        {
            if (quest.questStatus == QuestStatus.Progress)
            {
                activeQuests.Add(quest);
            }
        }
        return activeQuests;
    }
}