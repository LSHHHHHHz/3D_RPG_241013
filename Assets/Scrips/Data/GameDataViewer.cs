using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class GameDataViewer : MonoBehaviour
{
    public static GameDataViewer instance;
    public GameData data;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        data = GameData.instance;
    }
    private void OnDisable()
    {
        data.Save();
    }
}