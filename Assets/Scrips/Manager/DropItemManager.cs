using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    IReadOnlyList<BaseEnemy> baseEnemies;
    Vector3 enemyDeathPos;
    HashSet<BaseEnemy> registeredEnemies = new HashSet<BaseEnemy>();
    string enemyID;
    string[] dropItemsPrefabsPath;
    private void Awake()
    {
        baseEnemies = ActorManager<BaseEnemy>.instnace.GetActors();
        dropItemsPrefabsPath = new string[]
         {
            "Dropitem/EquipmentDropItem",
            "Dropitem/PortionDropItem",
            "Dropitem/CoinDropItem",
            "Dropitem/ExpDropItem"
         };
    }
    private void OnEnable()
    {
        StartCoroutine(RegisterEnemies());
    }

    private void OnDisable()
    {
        StopCoroutine(RegisterEnemies());

        foreach (var enemy in registeredEnemies)
        {
            enemy.onDeathEnemyID -= DropRewardItem;
        }
        registeredEnemies.Clear();
    }
    IEnumerator RegisterEnemies()
    {
        while (true)
        {
            baseEnemies = ActorManager<BaseEnemy>.instnace.GetActors();
            foreach (var enemy in baseEnemies)
            {
                if (!registeredEnemies.Contains(enemy))
                {
                    enemy.onDeathEnemyID += DropRewardItem;
                    registeredEnemies.Add(enemy);
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void DropRewardItem(Vector3 pos, string id)
    {
        enemyDeathPos = pos;
        int count = UnityEngine.Random.Range(4, 6);
        for (int i = 0; i < count; i++)
        {
            DropItem dropItem = GameManager.instance.poolManager.GetObjectFromPool(dropItemsPrefabsPath[MonsterDeathDropItemIndex()]).GetComponent<DropItem>();
            dropItem.CreatedDropItem(enemyDeathPos);
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
