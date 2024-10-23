using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int EnemyHP = 30;
    public void GetHP(int amount)
    {
    }
    public void ReduceHP(int amount)
    {
    }
}