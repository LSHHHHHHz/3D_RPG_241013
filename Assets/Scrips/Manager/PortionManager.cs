using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Portion
{
    public string id { get; private set; }
    public int recoveryAmount { get; private set; }

    public Portion(string id, int recoveryAmount)
    {
        this.id = id;
        this.recoveryAmount = recoveryAmount;
    }
    public void Use()
    {
        Debug.Log(recoveryAmount + " : È¸º¹");
    }
}
public class PortionManager : MonoBehaviour
{
    List<Portion> portions = new List<Portion>();

    private void Awake()
    {
        SetPortionData();
    }
    void SetPortionData()
    {
        portions.Add(new Portion("HPPortion1", 10));
    }
    public Portion GetPortion(string id)
    {
        foreach (var portion in portions)
        {
            if (portion.id == id) 
            {
                return portion;
            }
        }
        return null;
    }
}