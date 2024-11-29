using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    string enemyID;
    string[] dropItemsPrefabsPath;
    private void Awake()
    {
        dropItemsPrefabsPath = new string[]
         {
            "Dropitem/EquipmentDropItem",
            "Dropitem/PortionDropItem",
            "Dropitem/CoinDropItem",
            "Dropitem/ExpDropItem"
         };
    }
    public void DropItem(Vector3 pos, string id, EnemyType type)
    {
        int count = 0;
        if (type == EnemyType.Normar)
        {
            count = UnityEngine.Random.Range(4, 6);
        }
        else
        {
            count = UnityEngine.Random.Range(7, 10);
        }
        for (int i = 0; i < count; i++)
        {
            DropItem dropItem = GameManager.instance.poolManager.GetObjectFromPool(dropItemsPrefabsPath[MonsterDeathDropItemIndex()]).GetComponent<DropItem>();
            dropItem.CreatedDropItem(pos);
            dropItem.SetData(id);
        }
    }
    int MonsterDeathDropItemIndex()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue > 0.95f)
        {
            return 0;
        }
        else if (randomValue > 0.8f)
        {
            return 1;
        }
        else if (randomValue > 0.4f)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
