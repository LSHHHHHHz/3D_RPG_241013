using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

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
    void ClearMainQuest()
    {

    }
    void ClearSubQuest()
    {

    }
}
