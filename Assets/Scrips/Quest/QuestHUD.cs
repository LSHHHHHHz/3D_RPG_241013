using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestHUD : MonoBehaviour
{
    [SerializeField] QuestHUDSlotUI[] questHUDSlotUIs;
    private void Start()
    {
        SetData(QuestManager.instance.currentQuest);
    }
    private void OnEnable()
    {
        QuestManager.instance.onStartQuest += SetData;
        QuestManager.instance.onFinishQuest += SetLastData;
    }
    private void OnDisable()
    {
        QuestManager.instance.onStartQuest -= SetData;
        QuestManager.instance.onFinishQuest -= SetLastData;
    }
    void SetData(Quest quest)
    {
        int goalCount = quest.goal.goalName.Length - 1;
        for (int i = 0; i < questHUDSlotUIs.Length; i++)
        {
            if (i < goalCount)
            {
                QuestManager.instance.onClearGoalQeust += questHUDSlotUIs[i].ClearQuestUI;
                questHUDSlotUIs[i].gameObject.SetActive(true);
                questHUDSlotUIs[i].SetQuestData(quest.goal.goalName[i]);
            }
            else
            {
                questHUDSlotUIs[i].gameObject.SetActive(false);
            }
        }
    }
    void SetLastData(Quest quest)
    {
        int goalCount = quest.goal.goalName.Length;
        for (int i = 0; i < questHUDSlotUIs.Length; i++)
        {
            if (i == goalCount)
            {
                QuestManager.instance.onClearGoalQeust += questHUDSlotUIs[i].ClearQuestUI;
                questHUDSlotUIs[i].gameObject.SetActive(true);
                questHUDSlotUIs[i].SetQuestData(quest.goal.goalName[i]);
            }
            else
            {
                questHUDSlotUIs[i].gameObject.SetActive(false);
            }
        }
    }
}
