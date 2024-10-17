using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<List<GameObject>> pools;
    string preGameObjectPrefabPath;
    GameObject preGameObject;
    private void Awake()
    {
        pools = new List<List<GameObject>>();
        for (int i = 0; i < prefabs.Count; i++)
        {
            pools.Add(new List<GameObject>());
        }
    }
    public GameObject GetObjectFromPool(string gameObjectPath)
    {
        if (preGameObjectPrefabPath != gameObjectPath)
        {
            preGameObjectPrefabPath = gameObjectPath;
            preGameObject = Resources.Load<GameObject>(gameObjectPath);
        }
        int index = -1;
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i] == preGameObject)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            prefabs.Add(preGameObject);
            pools.Add(new List<GameObject>());
            index = prefabs.Count - 1;
        }
        if (preGameObject == null)
        {
            return null;
        }
        GameObject select = null;
        foreach (var obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                return select;
            }
        }
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}