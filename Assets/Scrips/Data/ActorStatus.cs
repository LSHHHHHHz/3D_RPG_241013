using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats
{
    public int playerTotalAttack { get; private set; }
    public event Action<int> onChangeAttack;

    public PlayerStats(int initialAttack)
    {
        playerTotalAttack = initialAttack;
    }
    public void InitializeStats()
    {
        onChangeAttack?.Invoke(playerTotalAttack);
    }
    public void IncreaseAttack(int amount)
    {
        playerTotalAttack += amount;
        onChangeAttack?.Invoke(playerTotalAttack);
    }
    public void DecreaseAttack(int amount)
    {
        playerTotalAttack -= amount;
        onChangeAttack?.Invoke(playerTotalAttack);
    }
}
public class PlayerStatus
{
    public int playerMaxHP { get; private set; }
    public int playerCurrentHP { get; private set; }
    public int playerMaxMP { get; private set; }
    public int playerCurrentMP { get; private set; }
    public float originSpeed { get; private set; }
    public float currentMoveSpeed { get; private set; }
    public int playerMaxExp { get; private set; }
    public int playerCurrentExp { get; private set; }
    public int playerLevel { get; private set; }

    public event Action<int,int> onChangeHP;
    public event Action<int,int> onChangeMP;
    public event Action<int,int> onChangeExp;
    public event Action<int> onChangeLevel;
    public PlayerStatus(int maxHp, int maxMp)
    {
        playerMaxHP = maxHp;
        playerCurrentHP = playerMaxHP;
        playerMaxMP = maxMp;
        playerCurrentMP = playerMaxMP;
        playerMaxExp = 100; 
        playerCurrentExp = 0;
        playerLevel = 1;
    }
    public void IntializeStatus()
    {
        onChangeHP?.Invoke(playerCurrentHP, playerMaxHP);
        onChangeMP?.Invoke(playerMaxMP, playerMaxMP);
        onChangeExp?.Invoke(playerCurrentMP, playerMaxExp);
        onChangeLevel?.Invoke(playerLevel);
    }
    public void GetHP(int amount)
    {
        playerCurrentHP += amount;
        onChangeHP?.Invoke(playerCurrentHP, playerMaxExp);
    }
    public void ReduceHP(int amount)
    {
        playerCurrentHP -= amount;
        onChangeHP?.Invoke(playerCurrentHP, playerMaxExp);
    }
    public void GetMP(int amount)
    {
        playerCurrentMP += amount;
        onChangeMP?.Invoke(playerCurrentMP, playerMaxExp);
    }
    public void ReduceMP(int amount)
    {
        playerCurrentMP -= amount;
        onChangeMP?.Invoke(playerCurrentMP, playerMaxExp);
    }
    public void GetExp(int amount)
    {
        playerCurrentExp += amount;
        onChangeExp?.Invoke(playerCurrentExp, playerMaxExp);

        while (playerCurrentExp >= playerMaxExp)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        playerLevel++;
        playerMaxExp += playerLevel * 100; 
        playerCurrentExp -= playerMaxExp;
        onChangeLevel?.Invoke(playerLevel);
    }
}
public class EnemyStatus
{
    public int enemyMaxHP { get; private set; }
    public int enemyCurrentHP { get; private set; }

    public event Action<int, int> onChangeHP;
    public event Action onEnemyDeath;

    public EnemyStatus(int maxHp)
    {
        enemyMaxHP = maxHp;
        enemyCurrentHP = enemyMaxHP;
    }
    public void InitializeStatus()
    {
        onChangeHP?.Invoke(enemyCurrentHP, enemyMaxHP);
    }
    public void GetHP(int amount)
    {
        enemyCurrentHP += amount;
        if (enemyCurrentHP > enemyMaxHP)
        {
            enemyCurrentHP = enemyMaxHP;
        }
        onChangeHP?.Invoke(enemyCurrentHP, enemyMaxHP);
    }
    public void ReduceHP(int amount)
    {
        enemyCurrentHP -= amount;
        if (enemyCurrentHP < 0)
        {
            enemyCurrentHP = 0;
        }
        onChangeHP?.Invoke(enemyCurrentHP, enemyMaxHP);

        if (enemyCurrentHP <= 0)
        {
            onEnemyDeath?.Invoke();
        }
    }
}
