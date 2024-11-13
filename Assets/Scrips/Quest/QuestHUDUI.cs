using UnityEngine;
using UnityEngine.UI;
public class QuestHUDUI : MonoBehaviour
{
    public Text questNameText;
    public Text questProgressText;

    public void SetUI(Quest quest)
    {
        questNameText.text = quest.questTitle;

        if (quest.goal != null && quest.goal.goalType == GoalType.KillMonsters)
        {
            questProgressText.text = (quest.goal.currentAmount / quest.goal.requiredAmount).ToString();
        }
        else
        {
            questProgressText.text = "";
        }
    }
    public void UpdateProgress(string quest)
    {
        questProgressText.text = quest;
    }
}