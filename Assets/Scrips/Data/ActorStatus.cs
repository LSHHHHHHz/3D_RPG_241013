using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public int playerHP = 30;
    public float moveSpeed { get; private set; }
    public float originSpeed { get; private set; }
    public void GetHP(int amount)
    {
    }
    public void ReduceHP(int amount)
    {
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