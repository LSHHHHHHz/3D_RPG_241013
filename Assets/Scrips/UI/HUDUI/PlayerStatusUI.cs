using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusUI : MonoBehaviour
{
    Player player;
    [SerializeField] Image hpFillAmountImage;
    [SerializeField] Image mpFillAmountImage;
    [SerializeField] Image expFillAmountImage;
    [SerializeField] Text hpText;      
    [SerializeField] Text mpText;   
    [SerializeField] Text levelText;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        player.status.onChangeHP += UpdateHPUI;
        player.status.onChangeMP += UpdateMPUI;
        player.status.onChangeExp += UpdateExpUI;
        player.status.onChangeLevel += UpdateLevelUI;
        player.status.InitializeStatus();
    }
    private void OnDisable()
    {
        player.status.onChangeHP -= UpdateHPUI;
        player.status.onChangeMP -= UpdateMPUI;
        player.status.onChangeExp -= UpdateExpUI;
        player.status.onChangeLevel -= UpdateLevelUI;
    }
    void UpdateHPUI(int currentHP, int maxHP)
    {
        hpText.text = currentHP + " / " + maxHP;
        hpFillAmountImage.fillAmount = (float)currentHP / maxHP;
    }
    void UpdateMPUI(int currentMP, int maxMP)
    {
        mpText.text = currentMP + " / " + maxMP;
        mpFillAmountImage.fillAmount = (float)currentMP / maxMP;
    }
    void UpdateExpUI(int currentExp, int maxExp)
    {
        expFillAmountImage.fillAmount = (float)currentExp / maxExp;
    }
    void UpdateLevelUI(int lv)
    {
        levelText.text = "Level: " + lv;
    }
}
