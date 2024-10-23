using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCurrency
{
    public int coin { get; private set; }

    public event Action<int> onChangeCoin;

    public PlayerCurrency(int initialCoin)
    {
        coin = initialCoin;
        onChangeCoin?.Invoke(coin);  
    }
    public void GetCoin(int amount)
    {
        coin += amount;
        onChangeCoin?.Invoke(coin);  
    }
    public void SpendCoin(int amount)
    {
        if (coin >= amount)  
        {
            coin -= amount;
            onChangeCoin?.Invoke(coin);  
        }
        else
        {
            Debug.Log("µ·¾øÀ½");  
        }
    }
    public void InitializeCurrency()
    {
        onChangeCoin?.Invoke(coin); 
    }
}