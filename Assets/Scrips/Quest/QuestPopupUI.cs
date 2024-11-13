using System.Collections.Generic;
using UnityEngine;
public class QuestPopupUI : MonoBehaviour
{
    public Transform questListParent; 
    public GameObject questItemPrefab;

    private void Start()
    {
        UpdateQuestList(QuestManager.instance.GetQuests());
    }

    public void UpdateQuestList(List<Quest> quests)
    {
        foreach (Quest quest in quests)
        {
            GameObject questItem = Instantiate(questItemPrefab, questListParent);
            QuestSlotUI questItemUI = questItem.GetComponent<QuestSlotUI>();
            questItemUI.SetUpdateUI(quest);
        }
    }
}