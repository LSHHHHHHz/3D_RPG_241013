using UnityEngine;
using UnityEngine.UI;

public class QuestSlotUI : MonoBehaviour
{
    public Text questTitleText;
    public Text questStatusText;
    public Text questProgressText;

    public void SetUpdateUI(Quest quest)
    {
        questTitleText.text = quest.questTitle;
        questStatusText.text = quest.questStatus.ToString();

        if (quest.goal != null)
        {
            questProgressText.text = (quest.goal.currentAmount / quest.goal.requiredAmount).ToString();
        }
        else
        {
            questProgressText.text = "¾øÀ½";
        }
    }
}
