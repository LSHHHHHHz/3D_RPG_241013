using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    BaseEnemy baseEnemy;

    private void Awake()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnEnable()
    {
        if (baseEnemy != null && baseEnemy.enemyStatus != null)
        {
            baseEnemy.enemyStatus.onChangeHP += UpdateHpBar;

            UpdateHpBar(baseEnemy.enemyStatus.enemyCurrentHP, baseEnemy.enemyStatus.enemyMaxHP);
        }
    }
    private void OnDisable()
    {
        if (baseEnemy != null && baseEnemy.enemyStatus != null)
        {
            baseEnemy.enemyStatus.onChangeHP -= UpdateHpBar;
        }
    }
    void UpdateHpBar(int currentHP, int maxHp)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHP / maxHp;
        }
    }
}
