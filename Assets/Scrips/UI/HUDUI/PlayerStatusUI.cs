using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image hpFillAmountImage;
    [SerializeField] Image mpFillAmountImage;
    [SerializeField] Image expFillAmountImage;
    [SerializeField] Text hpText;      
    [SerializeField] Text mpText;     
    [SerializeField] Text expText;  
    [SerializeField] Text levelText;

    private void OnEnable()
    {
        Debug.Log("PlayerStatusUI 먼저되나");
        player.status.onChangeHP += ChangeHPUI;
        player.status.onChangeMP += ChangeMPUI;
        player.status.onChangeExp += ChangeExpUI;
        player.status.onChangeLevel += ChangeLevelUI;
        player.status.IntializeStatus();
    }
    private void OnDisable()
    {
        player.status.onChangeHP -= ChangeHPUI;
        player.status.onChangeMP -= ChangeMPUI;
        player.status.onChangeExp -= ChangeExpUI;
        player.status.onChangeLevel -= ChangeLevelUI;
    }
    void ChangeHPUI(int currentHP, int maxHP)
    {
        hpText.text = currentHP + " / " + maxHP;
        hpFillAmountImage.fillAmount = (float)currentHP / maxHP;
    }
    void ChangeMPUI(int currentMP, int maxMP)
    {
        mpText.text = currentMP + " / " + maxMP;
        mpFillAmountImage.fillAmount = (float)currentMP / maxMP;
    }
    void ChangeExpUI(int currentExp, int maxExp)
    {
        expText.text = currentExp + " / " + maxExp;
        expFillAmountImage.fillAmount = (float)currentExp / maxExp;
    }
    void ChangeLevelUI(int lv)
    {
        levelText.text = "Level: " + lv;
    }
}
