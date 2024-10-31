using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossPortarPopup : MonoBehaviour
{
    int bossStage = -1;

    public void SetBossStageNum(int num)
    {
        bossStage = num;
    }
    public void EnterBossContents()
    {

    }
    public void CancelBossContents()
    {
        gameObject.SetActive(false);
    }
}

