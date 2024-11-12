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
        Debug.LogError(recoveryAmount + " : È¸º¹");
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
        portions.Add(new Portion("HPPortion1", 50));
        portions.Add(new Portion("HPPortion2", 100));
        portions.Add(new Portion("HPPortion3", 150));
        portions.Add(new Portion("HPPortion4", 200));
        portions.Add(new Portion("MPPortion1", 50));
        portions.Add(new Portion("MPPortion2", 100));
        portions.Add(new Portion("MPPortion3", 150));
        portions.Add(new Portion("MPPortion4", 200));
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