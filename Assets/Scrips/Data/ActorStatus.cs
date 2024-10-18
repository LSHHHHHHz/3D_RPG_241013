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

    public event Action<int> onChangeHP;
    public event Action<int> onChangeMP;
    public event Action<int> onChangeExp;
    public event Action<int> onChangeLevel;
    public PlayerStatus(int maxHp, int maxMp, float speed)
    {
        playerMaxHP = maxHp;
        playerCurrentHP = playerMaxHP;
        playerMaxMP = maxMp;
        playerCurrentMP = playerMaxMP;
        playerMaxExp = 100; 
        playerCurrentExp = 0;
        playerLevel = 1;
        originSpeed = speed;
        currentMoveSpeed = speed;
        onChangeHP?.Invoke(playerCurrentHP);
        onChangeMP?.Invoke(playerMaxMP);
        onChangeExp?.Invoke(playerCurrentMP);
        onChangeLevel?.Invoke(playerCurrentExp);
    }
    public void GetHP(int amount)
    {
        playerCurrentHP += amount;
        onChangeHP?.Invoke(playerCurrentHP);
    }
    public void ReduceHP(int amount)
    {
        playerCurrentHP -= amount;
        onChangeHP?.Invoke(playerCurrentHP);
    }
    public void GetMP(int amount)
    {
        playerCurrentMP += amount;
        onChangeMP?.Invoke(playerCurrentMP);
    }
    public void ReduceMP(int amount)
    {
        playerCurrentMP -= amount;
        onChangeMP?.Invoke(playerCurrentMP);
    }
    public void GainExp(int amount)
    {
        playerCurrentExp += amount;
        onChangeExp?.Invoke(playerCurrentExp);

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