using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShield : MonoBehaviour , IEquipment
{
    string shiledID;
    public void SetItemID(string id)
    {
        shiledID = id;
    }

    public string GetItemID()
    {
        return shiledID;
    }

    public void EquipItem(string id)
    {
        Debug.Log("������ ����");
    }

    public void UnEquipItem(string id)
    {
        Debug.Log("������ ����");
    }
}
