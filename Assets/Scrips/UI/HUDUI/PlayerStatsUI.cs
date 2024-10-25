using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatsUI : MonoBehaviour
{
    Player player;
    [SerializeField] Text totalDamageText;
    [SerializeField] Text hpText;
    [SerializeField] Text mpText;
    [SerializeField] Text expText;
    [SerializeField] Text coinText;
    [SerializeField] Text lvText;
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
        player.stats.onChangeAttack += UpdateAttackUI;
        player.currency.onChangeCoin += UpdateCoinUI;
        player.stats.InitializeStats();
    }
    private void OnDisable()
    {
        player.status.onChangeHP -= UpdateHPUI;
        player.status.onChangeMP -= UpdateMPUI;
        player.status.onChangeExp -= UpdateExpUI;
        player.status.onChangeLevel -= UpdateLevelUI;
        player.stats.onChangeAttack -= UpdateAttackUI;
        player.currency.onChangeCoin -= UpdateCoinUI;
    }
    void UpdateAttackUI(int totalAttack)
    {
        totalDamageText.text = "공격력 :" + totalAttack.ToString();
    }
    public void UpdateHPUI(int currentHP, int maxHP)
    {
        hpText.text = "HP : " + currentHP + " / " + maxHP;
    }
    public void UpdateMPUI(int currentMP, int maxMP)
    {
        mpText.text = "MP : " + currentMP + " / " + maxMP;
    }
    public void UpdateExpUI(int currentExp, int maxExp)
    {
        expText.text = "경험치 : " + currentExp + " / " + maxExp;
    }
    public void UpdateCoinUI(int coins)
    {
        coinText.text = "코인 : " + coins.ToString();
    }
    void UpdateLevelUI(int lv)
    {
        lvText.text = "레벨 : " + lv;
    }
}
